using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebBookShopAPI.Data.Interfaces;
using WebBookShopAPI.Data.Models;
using WebBookShopAPI.Data.Models.OrderEntities;
using WebBookShopAPI.Data.Specifications;

namespace WebBookShopAPI.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IGenericRepository<Delivery> _delRepo;
        private readonly IGenericRepository<Book> _bookRepo;
        private readonly IGenericRepository<OrderStatus> _orderStatusRepo;
        private readonly IBasketRepository _basketRepo;
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context, IGenericRepository<Order> orderRepo, IGenericRepository<Delivery> delRepo,
            IGenericRepository<Book> bookRepo, IGenericRepository<OrderStatus> orderStatusRepo, IBasketRepository basketRepo)
        {
            _orderRepo = orderRepo;
            _delRepo = delRepo;
            _bookRepo = bookRepo;
            _basketRepo = basketRepo;
            _context = context;
            _orderStatusRepo = orderStatusRepo;
        }

        public async Task<Order> CreateOrderAsync(string ContactEmail, string ContactName, string ContactPhone, int deliveryId, string Address, string UserId, string basketId)
        {
            // get basket from the basket repo
            var basket = await _basketRepo.GetBasketAsync(basketId);
            if (basket == null) return null;

            // get items from the book repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var bookItem = await _bookRepo.GetByIdAsync(item.Id);
                var orderItem = new OrderItem(bookItem.Id, item.Quantity, bookItem.Price);
                items.Add(orderItem);
            }

            // get delivery method
            var deliveryMethod = await _delRepo.GetByIdAsync(deliveryId);

            // calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Amount);
            subtotal = subtotal + deliveryMethod.Price;


            // default values
            if (Address.IsNullOrEmpty()) Address = "проспект Людвіга Свободи, 33, Харків, Харківська область, 61000";
            if (UserId.IsNullOrEmpty()) UserId = "Guest";

            // create order
            var order = new Order(items, ContactName, ContactEmail, ContactPhone, Address, subtotal, deliveryId, UserId);

            // save to db
            await _context.Order.AddAsync(order);

            // delete basket
            await _basketRepo.DeleteBasketAsync(basketId);

            await _context.SaveChangesAsync();

            return order;
        }


        public async Task<Order> GetOrderByIdAsync(int OrderId)
        {

            var order = await _context.Order
                .Include(n => n.OrderItem)
                .ThenInclude(n => n.Book)
                .Include(n => n.Delivery)
                .Include(n => n.OrderStatus)
                .Where(n => n.Id == OrderId)
                .FirstOrDefaultAsync();

            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string UserId)
        {

            var orders = await _context.Order
                .Include(n => n.OrderItem)
                .ThenInclude(n => n.Book)
                .Include(n => n.Delivery)
                .Include(n => n.OrderStatus)
                .Where(n => n.AppUserId == UserId)
                .ToListAsync();

            return orders;
        }

        public async Task<IReadOnlyList<Delivery>> GetDeliveryMethodsAsync()
        {
            var delMethods = await _context.Delivery.ToListAsync();
            return delMethods;
        }

        public async Task<bool> ChangeOrderStatusAsync(int orderId, int orderStatusId)
        {
            var order = await _context.Order.FirstOrDefaultAsync(x => x.Id == orderId);
            if (order == null) return false;
            if (order.OrderStatusId != 1 && orderStatusId == 8) return false;

            order.OrderStatusId = orderStatusId;
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<IReadOnlyList<Order>> GetOrdersForAdminAsync()
        {
            var orders = await _context.Order
                    .Include(n => n.OrderItem)
                    .ThenInclude(n => n.Book)
                    .Include(n => n.Delivery)
                    .Include(n => n.OrderStatus)
                    .ToListAsync();

            return orders;
        }
    }
}
