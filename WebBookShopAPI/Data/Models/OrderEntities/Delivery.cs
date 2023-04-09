namespace WebBookShopAPI.Data.Models.OrderEntities
{
    public class Delivery : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<Order> Order { get; set; }

    }
}
