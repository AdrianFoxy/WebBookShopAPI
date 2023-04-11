using Microsoft.EntityFrameworkCore;
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

        public async Task<Order> CreateOrderAsync(string ContactEmail, string ContactPhone, int deliveryId, string Address, string UserId, string basketId)
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
            var orderStatus = await _orderStatusRepo.GetByIdAsync(1);

            // calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Amount);
            subtotal = subtotal + deliveryMethod.Price;


            // default address
            if (Address.Length <= 0) Address = "проспект Людвіга Свободи, 33, Харків, Харківська область, 61000";

            // create order
            var order = new Order(items, ContactEmail, ContactPhone, Address, subtotal, deliveryId, UserId, orderStatus);

            // save to db
            await _context.Order.AddAsync(order);

                // delete basket
                await _basketRepo.DeleteBasketAsync(basketId);

            await _context.SaveChangesAsync();

            return order;
        }


        public async Task<Order> GetOrderByIdAsync(int OrderId, string UserId)
        {
            var spec = new OrderWithItemsAndOrderingSpecification(OrderId, UserId);

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
            var spec = new OrderWithItemsAndOrderingSpecification(UserId);

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
