using System.Text.Json.Serialization;

namespace WebBookShopAPI.Data.Models.OrderEntities
{
    public class Delivery : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public List<Order> Order { get; set; }

    }
}
