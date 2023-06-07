using Microsoft.EntityFrameworkCore;
using WebBookShopAPI.Data.Dtos;
using WebBookShopAPI.Data.Interfaces;
using WebBookShopAPI.Data.Models;
using WebBookShopAPI.Migrations;

namespace WebBookShopAPI.Data.Repositories
{
    public class BookRepository :  IBookRepository
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

        public async Task<List<AuthorWithBookCount>> GetFavoriteAuthors(string userId)
        {
            var favoriteAuthors = await _context.OrderItem
                .Where(n => n.Order.AppUserId == userId)
                .SelectMany(n => n.Book.Author)
                .GroupBy(a => a)
                .Select(g => new AuthorWithBookCount
                {
                    Author = g.Key,
                    BookCount = g.Count()
                })
                .OrderByDescending(n => n.BookCount)
                .ToListAsync();

            return favoriteAuthors;
        }

        public async Task<List<GenreWithBookCount>> GetFavoriteGenres(string userId)
        {
            var favoriteGenres = await _context.OrderItem
                .Where(n => n.Order.AppUserId == userId)
                .SelectMany(n => n.Book.Genre)
                .GroupBy(a => a)
                .Select(g => new GenreWithBookCount
                {
                    Genre = g.Key,
                    BookCount = g.Count()
                })
                .OrderByDescending (n => n.BookCount)
                .ToListAsync();

            return favoriteGenres;
        }

        public async Task<List<Book>> GetPurchasedBooks(string userId)
        {
            var purchasedBooks = await _context.OrderItem
                .Where(n => n.Order.AppUserId == userId)
                .Select(n => n.Book)
                .Distinct()
                .ToListAsync();

            return purchasedBooks;
        }

        public int CalculateAge(DateTime dateOfBirth)
        {
            DateTime currentDate = DateTime.Now;

            int age = currentDate.Year - dateOfBirth.Year;

            return age;
        }


        public async Task<IReadOnlyList<Book>> GetRecommedantiosByAgeGroup(string userId)
        {
            int ageFrom, ageTo = 0;
            var user = _context.Users.Include(g => g.Gender).FirstOrDefault(u => u.Id == userId);
            int userAge = CalculateAge(user.DateOfBirth);
            string gender = user.Gender.GenderCode;
            if (userAge >= 0 && userAge <= 16) { ageFrom = 0; ageTo = 16; }
            else if (userAge >= 16 && userAge <= 21) { ageFrom = 16; ageTo = 21; }
            else if(userAge >= 21 && userAge <= 35) { ageFrom = 21; ageTo = 35; }
            else if(userAge >=35 && userAge <= 55 ) { ageFrom = 35; ageTo = 55; }
            else { ageFrom = 51; ageTo = 1000; }

            var purchasedBooks = await GetPurchasedBooks(userId);
            var recommedationsBeforeExcept = await _context.OrderItem
                .Where(oi => EF.Functions.DateDiffYear(oi.Order.AppUser.DateOfBirth, DateTime.Now) >= ageFrom &&
                    EF.Functions.DateDiffYear(oi.Order.AppUser.DateOfBirth, DateTime.Now) <= ageTo)
                .Include(oi => oi.Book.Author)
                .Select(oi => oi.Book)
                .ToListAsync();

            var recommedations = recommedationsBeforeExcept.Except(purchasedBooks).Take(12).ToList();

            return recommedations;
        }

        public async Task<IReadOnlyList<Book>> GetRecommedantiosByOrders(string userId, DateTime? startDate, DateTime? endDate)
        {
            // Get purchased books
            var purchasedBooks = await GetPurchasedBooks(userId);

            // Get favAuthors
            var favoriteAuthors = await GetFavoriteAuthors(userId);

            // Get favGenres
            var favoriteGenres = await GetFavoriteGenres(userId);

            // Get int lists, of favAuthors and favGenres
            List<int> authorsIds = favoriteAuthors.Select(a => a.Author.Id).ToList();
            List<int> genresIds = favoriteGenres.Select(a => a.Genre.Id).ToList();

            // Get books by favAuthors and favGenres
            var booksByAuthors = await _context.Book
                .Include(a => a.Author)
                .Where(book => book.Author.Any(author => authorsIds.Contains(author.Id)))
                .Where(book => book.UploadedInfo >= startDate && book.UploadedInfo <= endDate)
                .ToListAsync();

            var booksByGenres = await _context.Book
                .Include(a => a.Author)
                .Where(book => book.Genre.Any(genre => genresIds.Contains(genre.Id)))
                .Where(book => book.UploadedInfo >= startDate && book.UploadedInfo <= endDate)
                .ToListAsync();

            // Merge booksByAuthors and booksByGenres, by union and except purchasedBooks
            var recommedations = booksByAuthors.Union(booksByGenres).Except(purchasedBooks).Take(12).ToList();

            return recommedations;
        }

        public async Task<IReadOnlyList<Book>> GetRecommedantionsBestSells()
        {

            var bestSellingBooks = await _context.OrderItem
                .GroupBy(oi => oi.BookId)
                .Select(g => new
                {
                    BookId = g.Key,
                    TotalAmount = g.Sum(oi => oi.Amount)
                })
                .OrderByDescending(g => g.TotalAmount)
                .Take(12)
                .Join(
                    _context.Book,
                    g => g.BookId,
                    book => book.Id,
                    (g, book) => book
                )
                .Include(a => a.Author)
                .ToListAsync();

            return bestSellingBooks;
        }

        public async Task<IReadOnlyList<Book>> GetRecommedationsRandom()
        {
            var books = await _context.Book.Include(a => a.Author).ToListAsync();

            Random rnd = new Random();
            List<Book> bookslist = new List<Book>();
            var count = books.Count();
            while(bookslist.Count() != 12)
            {
                int randId = rnd.Next(0, count);
                var result = books.ElementAt(randId);
                if(!bookslist.Contains(result) && result != null)
                    bookslist.Add(result);

            }

            return bookslist;
        }
    }
}
