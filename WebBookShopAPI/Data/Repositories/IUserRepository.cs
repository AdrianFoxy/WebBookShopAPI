using WebBookShopAPI.Data.Models;
using WebBookShopAPI.Data.Models.Identity;

namespace WebBookShopAPI.Data.Repositories
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<AppUser>> GetAllUsersAsync();

    }
}
