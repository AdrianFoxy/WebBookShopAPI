using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebBookShopAPI.Data.Models.Identity;

namespace WebBookShopAPI.Data.Models
{
    public class Review : BaseEntity
    {
        public Review() { }
        public string Review_text { get; set; }
        public int Rating { get; set; }
        public int BookId { get; set; }
        [JsonIgnore]
        public Book Book { get; set; }
        [JsonIgnore]
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }
}
