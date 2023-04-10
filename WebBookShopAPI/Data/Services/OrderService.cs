using System.Net;
using WebBookShopAPI.Data.Interfaces;
using WebBookShopAPI.Data.Models;
using WebBookShopAPI.Data.Models.OrderEntities;

namespace WebBookShopAPI.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IGenericRepository<Delivery> _delRepo;
        private readonly IGenericRepository<Book> _bookRepo;
        private readonly IBasketRepository _basketRepo;

        public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<Delivery> delRepo, 
            IGenericRepository<Book> bookRepo, IBasketRepository basketRepo) 
        {
            _orderRepo = orderRepo;
            _delRepo = delRepo;
            _bookRepo = bookRepo;
            _basketRepo = basketRepo;
        }

        public async Task<Order> CreateOrderAsync(string ContactEmail, string ContactPhone, int deliveryId, string Address, string UserId, string basketId)
        {
            // get basket from the basket repo
            var basket = await _basketRepo.GetBasketAsync(basketId);

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
            // create order
            var order = new Order(items, ContactEmail, ContactPhone, Address, subtotal, deliveryId, UserId);
            // save to db

            // return order
            return order;
        }

        public Task<Order> GetOrderByIdAsync(int OrderId, string UserId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string UserId)
        {
            throw new NotImplementedException();
        }
    }
}
