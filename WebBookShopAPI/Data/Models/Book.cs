using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBookShopAPI.Data.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Page { get; set; }
        public string Format { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public int Amount { get; set; }
        public int ReleaseYear { get; set; }
        public DateTime UploadDate { get; set; }

        public int BookSeriesId { get; set; }
        [ForeignKey("BookSeriesId")]
        public BookSeries BookSeries { get; set; }
        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }

        public List<Author> Author { get; set; }

        public List<Genre> Genre { get; set; }
    }
}
