using System.Text.Json.Serialization;
using WebBookShopAPI.Data.Models.Identity;

namespace WebBookShopAPI.Data.Models
{
    public class UserSelectedBook
    {
        public int BookId { get; set; }
        [JsonIgnore]
        public Book Book { get; set; }
        public string AppUserId { get; set; }
        [JsonIgnore]
        public AppUser AppUser { get; set; }
        
    }
}
