using System.Text.Json.Serialization;

namespace WebBookShopAPI.Data.Models.OrderEntities
{
    public class OrderStatus : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public List<Order> Order { get; set; }
    }
}
