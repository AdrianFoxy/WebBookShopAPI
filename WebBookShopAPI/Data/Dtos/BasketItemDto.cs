using System.ComponentModel.DataAnnotations;

namespace WebBookShopAPI.Data.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Ціна має бути вище за нуль")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage ="Вам потрібен хоча б один товар")]
        public int Quantity { get; set; }
        [Required]
        public string ImageURL { get; set; }
        [Required]
        public List<string> Authors { get; set; }
    }
}
