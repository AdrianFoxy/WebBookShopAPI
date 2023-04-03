using WebBookShopAPI.Data.Models.Identity;

namespace WebBookShopAPI.Data.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
