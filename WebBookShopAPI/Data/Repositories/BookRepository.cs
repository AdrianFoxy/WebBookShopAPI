using Microsoft.EntityFrameworkCore;
using WebBookShopAPI.Data.Interfaces;
using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private AppDbContext _context;
        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Book>> GetAllBooksCatalogAsync()
        {

            return await _context.Book
                .Include(n => n.Publisher)
                .Include(n => n.Author)
                .Include(n => n.BookSeries)
                .Include(n => n.Genre)
                .ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Book
                .Include(n => n.Publisher)
                .Include(n => n.Author)
                .Include(n => n.BookSeries)
                .Include(n => n.Genre)
                .FirstOrDefaultAsync(n => n.Id == id);
        }
    }
}
