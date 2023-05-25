using WebBookShopAPI.Data.Models;
using WebBookShopAPI.Data.Models.Identity;

namespace WebBookShopAPI.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<AppUser>> GetAllUsersAsync();

    }
}
