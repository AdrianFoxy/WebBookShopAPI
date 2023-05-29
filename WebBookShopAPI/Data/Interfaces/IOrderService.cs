using WebBookShopAPI.Data.Models.OrderEntities;

namespace WebBookShopAPI.Data.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string ContactName, string ContactEmail, string contacntPhone, int deliveryId, string address, string UserId, string basketId);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string UserId);
        Task<IReadOnlyList<Order>> GetOrdersForAdminAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<IReadOnlyList<Delivery>> GetDeliveryMethodsAsync();
        Task<bool> ChangeOrderStatusAsync(int orderId, int orderStatusId);
        
    }
}
