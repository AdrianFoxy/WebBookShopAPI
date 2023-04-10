﻿using WebBookShopAPI.Data.Models.OrderEntities;

namespace WebBookShopAPI.Data.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string ContactEmail, string contacntPhone, int deliveryId, string address, string UserId, string basketId);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string UserId);
        Task<Order> GetOrderByIdAsync(int id, string UserId);
        
    }
}
