using System.ComponentModel.DataAnnotations.Schema;

namespace WebBookShopAPI.Data.Models.OrderEntities
{
    public class OrderItem
    {
        public int Amount { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

    }
}
