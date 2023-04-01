using Microsoft.AspNetCore.Identity;
using WebBookShopAPI.Data.Models.Identity;

namespace WebBookShopAPI.Data
{
    public class AppDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    FullName = "User",
                    Email = "user@gmail.com",
                    UserName = "user@gmail.com",

                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
