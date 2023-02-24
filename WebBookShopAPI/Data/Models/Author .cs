using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebBookShopAPI.Data.Models
{
    public class Author : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Book> Book { get; set; }
    }
}
