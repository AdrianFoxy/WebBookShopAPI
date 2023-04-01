namespace WebBookShopAPI.Data.Dtos
{
    public class BookInCatalogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public float Price { get; set; }
        public float Rating { get; set; }
        public DateTime UploadedInfo { get; set; }
        public List<string> Authors { get; set; }
    }
}
