using System.Security.Claims;

namespace WebBookShopAPI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string RetrieveIdFromPrincipal(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
