using System.Text.Json.Serialization;

namespace WebBookShopAPI.Data.Models
{
    public class SelectionOfBooks : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        [JsonIgnore]
        public List<Book> Book { get; set; }
    }
}
