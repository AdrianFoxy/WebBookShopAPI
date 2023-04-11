using WebBookShopAPI.Data.Models.OrderEntities;

namespace WebBookShopAPI.Data.Specifications
{
    public class OrderWithItemsAndOrderingSpecification : BaseSpecification<Order>
    {
        public OrderWithItemsAndOrderingSpecification(string userId): base(o => o.AppUserId == userId)
        {
            AddInclude(o => o.OrderItem);
            AddInclude(o => o.Delivery);
            AddInclude(o => o.OrderStatus);
            AddOrderByDescending(o => o.UploadedInfo);
        }

        public OrderWithItemsAndOrderingSpecification(int id, string userId)
            : base(o => o.Id == id && o.AppUserId == userId)
        {
            AddInclude(o => o.OrderItem);
            AddInclude(o => o.Delivery);
            AddInclude(o => o.OrderStatus);
        }
    }
}
