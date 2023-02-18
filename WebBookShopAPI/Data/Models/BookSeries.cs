using System.Text.Json.Serialization;

namespace WebBookShopAPI.Data.Models
{
    public class BookSeries : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public List<Book> Book { get; set; }
    }
}
