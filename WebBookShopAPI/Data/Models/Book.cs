using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebBookShopAPI.Data.Models.OrderEntities;

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
        public decimal Price { get; set; }
        public int ReleaseYear { get; set; }
        public float Rating { get; set; }
        public int BookSeriesId { get; set; }
        [ForeignKey("BookSeriesId")]
        public BookSeries BookSeries { get; set; }
        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
        public List<Author> Author { get; set; }
        public List<Genre> Genre { get; set; }
        public List<SelectionOfBooks> SelectionOfBooks { get; set; }
        [JsonIgnore]
        public List<OrderItem> OrderItem { get; set; }
        [JsonIgnore]
        public List<UserSelectedBook> UserSelectedBook { get; set; }

    }
}
