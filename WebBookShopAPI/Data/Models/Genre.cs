using System.Text.Json.Serialization;

namespace WebBookShopAPI.Data.Models
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public List<Book> Book { get; set; }
    }
}
