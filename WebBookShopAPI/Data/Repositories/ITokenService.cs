using WebBookShopAPI.Data.Models.Identity;

namespace WebBookShopAPI.Data.Repositories
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
