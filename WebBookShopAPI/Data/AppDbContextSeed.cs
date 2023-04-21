using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebBookShopAPI.Data.Models.Identity;

namespace WebBookShopAPI.Data
{
    public class AppDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {     


            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));


            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    FullName = "User",
                    Email = "user@gmail.com",
                    UserName = "user@gmail.com",
                    DateOfBirth = new DateTime(2001, 11, 27),
                    PhoneNumber = "1234567890",
                    UserGenderCode = "M"

                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, UserRoles.User);

                var userF = new AppUser
                {
                    FullName = "UserF",
                    Email = "userF@gmail.com",
                    UserName = "userF@gmail.com",
                    DateOfBirth = new DateTime(2001, 11, 27),
                    PhoneNumber = "1234567890",
                    UserGenderCode = "F"

                };

                await userManager.CreateAsync(userF, "Pa$$w0rd");
                await userManager.AddToRoleAsync(userF, UserRoles.User);

                var admin = new AppUser
                {
                    FullName = "Admin",
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    DateOfBirth = new DateTime(2001, 11, 27),
                    PhoneNumber = "1234567890",
                    UserGenderCode = "M"

                };

                await userManager.CreateAsync(admin, "Pa$$w0rd");
                await userManager.AddToRoleAsync(admin, UserRoles.Admin);

                var guest = new AppUser
                {
                    Id = "Guest",
                    FullName = "Guest",
                    Email = "guest@gmail.com",
                    UserName = "guest@gmail.com",
                    DateOfBirth = new DateTime(2001, 11, 27),
                    PhoneNumber = "1234567890",
                    UserGenderCode = "M"

                };

                await userManager.CreateAsync(guest, "Pa$$w0rd");
                await userManager.AddToRoleAsync(guest, UserRoles.User);
            }
        }
    }
}
