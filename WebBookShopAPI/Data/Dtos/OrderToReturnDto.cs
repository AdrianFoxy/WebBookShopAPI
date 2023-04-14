using System.ComponentModel.DataAnnotations.Schema;

namespace WebBookShopAPI.Data.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime UploadedInfo { get; set; }
        [Column(TypeName = "DateTime")]
        public DateTime UpdatedInfo { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }
        public decimal Sum { get; set; }
        public int DeliveryId { get; set; }
        [ForeignKey("DeliveryId")]
        public DeliveryDto Delivery { get; set; }
        public OrderStatusDto OrderStatus { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItem { get; set; }
        public string AppUserId { get; set; }
    }
}
