namespace WebBookShopAPI.Data.Models.OrderEntities
{
    public class OrderStatus : BaseEntity
    {
        public string Name { get; set; }
        public List<Order> Order { get; set; }
    }
}
