using Microsoft.EntityFrameworkCore;
using WebBookShopAPI.Data.Interfaces;
using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private AppDbContext _context;
        public GenreRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<Genre>> GetAllGenresAsync()
        {
            return await _context.Genre.ToListAsync();
        }
    }
}
