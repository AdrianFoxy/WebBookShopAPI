using System.ComponentModel.DataAnnotations.Schema;
using WebBookShopAPI.Data.Models.Identity;

namespace WebBookShopAPI.Data.Models.OrderEntities
{
    public class Order : BaseEntity
    {
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }
        public decimal Sum { get; set; }
        public int DeliveryId { get; set; }
        [ForeignKey("DeliveryId")]
        public Delivery Delivery;
        public int OrderStatusId { get; set; }
        [ForeignKey("OrderStatusId")]
        public OrderStatus OrderStatus { get; set; }
        public List<OrderItem> OrderItem { get; set; }
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public decimal GetTotal()
        {
            return Sum + Delivery.Price;
        }
    }
}
