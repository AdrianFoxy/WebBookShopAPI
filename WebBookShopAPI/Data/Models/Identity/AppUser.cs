using Microsoft.AspNetCore.Identity;

namespace WebBookShopAPI.Data.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string UserGenderCode { get; set; }
        public Gender Gender { get; set; }
    }
}
