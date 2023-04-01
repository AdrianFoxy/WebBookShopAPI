using System.ComponentModel.DataAnnotations.Schema;
using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data.Dtos
{
    public class SingleBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Page { get; set; }
        public string Format { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public int Amount { get; set; }
        public float Price { get; set; }
        public int ReleaseYear { get; set; }
        public DateTime UploadedInfo { get; set; }
        public DateTime UpdatedInfo { get; set; }
        public BookSeries BookSeries { get; set; }
        public Publisher Publisher { get; set; }
        public List<Author> Author { get; set; }
        public List<Genre> Genre { get; set; }
    }
}
