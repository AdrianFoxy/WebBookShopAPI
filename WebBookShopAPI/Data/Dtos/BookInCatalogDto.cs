namespace WebBookShopAPI.Data.Dtos
{
    public class BookInCatalogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string BookSeries { get; set; }
        public List<string> Authors { get; set; }
    }
}
