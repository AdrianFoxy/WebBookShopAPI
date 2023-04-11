using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data.Dtos
{
    public class OrderItemDto
    {
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public int BookId { get; set; }
        public int OrderId { get; set; }
        public BookInOrderDto Book { get; set; }
        
    }
}
