using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using WebBookShopAPI.Data.Models.OrderEntities;

namespace WebBookShopAPI.Data.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string UserGenderCode { get; set; }
        public Gender Gender { get; set; }
        [JsonIgnore]
        public List<UserSelectedBook> UserSelectedBook { get; set; }

    }
}
