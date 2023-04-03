using Microsoft.AspNetCore.Identity;

namespace WebBookShopAPI.Data.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
