using System.ComponentModel.DataAnnotations;
using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
