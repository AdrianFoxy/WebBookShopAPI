using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebBookShopAPI.Data.Models.OrderEntities
{
    public class OrderItem
    {
        public OrderItem() { }


        public OrderItem(int BookId, int Amount, decimal price)
        {
            this.BookId = BookId;
            this.Amount = Amount;
            this.Price = price;
        }

        public int Amount { get; set; }
        public decimal Price { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        [JsonIgnore]
        public Book Book { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        [JsonIgnore]
        public Order Order { get; set; }

    }
}
