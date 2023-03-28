namespace WebBookShopAPI.Data.Dtos
{
    public class BookInShopCartDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public int Amount { get; set; }
        public float Price { get; set; }
        public List<string> Authors { get; set; }
    }
}
