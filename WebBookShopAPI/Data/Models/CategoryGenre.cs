using System.Text.Json.Serialization;

namespace WebBookShopAPI.Data.Models
{
    public class CategoryGenre : BaseEntity
    {
        public string Name { get; set; }

        [JsonIgnore]
        public List<Genre> Genre { get; set; }

    }
}
