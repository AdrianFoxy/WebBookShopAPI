using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data.Dtos
{
    public class ShopCartDto
    {
        public List<ShoppingCartItemDto> ShoppingCartItemDto { get; set; }
        public double ShopCartTotal { get; set; }
        public int TotalItems { get; set; }
    }
}
