namespace WebBookShopAPI.Data.Dtos
{
    public class ShoppingCartItemDto
    {
        public int Id { get; set; }
        public BookInShopCartDto BookInShopCartDto { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
