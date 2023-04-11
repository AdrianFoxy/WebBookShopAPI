using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebBookShopAPI.Data.Models.Identity;

namespace WebBookShopAPI.Data.Models.OrderEntities
{
    public class Order : BaseEntity
    {
        public Order() { }
        public Order(IReadOnlyList<OrderItem> OrderItems, string ContactEmail, string ContactPhone, string Address,
            decimal Sum, int DeliveryId, string UserId, OrderStatus OrderStatus) {
            this.OrderItem = OrderItems;
            this.ContactEmail = ContactEmail;
            this.ContactPhone = ContactPhone;
            this.Address = Address;
            this.Sum = Sum;
            this.DeliveryId = DeliveryId;
            this.AppUserId = UserId;
            this.OrderStatus = OrderStatus;
        }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }
        public decimal Sum { get; set; }
        public int DeliveryId { get; set; }
        [ForeignKey("DeliveryId")]
        public Delivery Delivery { get; set; }
        public int OrderStatusId { get; set; }
        [ForeignKey("OrderStatusId")]
        public OrderStatus OrderStatus { get; set; }
        public IReadOnlyList<OrderItem> OrderItem { get; set; }

        [JsonIgnore]
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }

        public decimal GetTotal()
        {
            return Sum + Delivery.Price;
        }
    }
}
