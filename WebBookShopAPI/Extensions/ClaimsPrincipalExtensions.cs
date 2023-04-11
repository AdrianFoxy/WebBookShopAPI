using System.Security.Claims;

namespace WebBookShopAPI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string RetrieveIdFromPrincipal(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static string RetrieveRoleFromPrincipal(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Role);
        }

        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}
