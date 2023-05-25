using Microsoft.EntityFrameworkCore;
using WebBookShopAPI.Data.Interfaces;
using WebBookShopAPI.Data.Models.Identity;

namespace WebBookShopAPI.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<AppUser>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
