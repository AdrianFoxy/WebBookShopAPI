using WebBookShopAPI.Data.Dtos;
using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetBookByIdAsync(int id);
        Task<IReadOnlyList<Book>> GetAllBooksCatalogAsync();
        Task<IReadOnlyList<Book>> GetRecommedantiosByAgeGroup(string userId);
        Task<IReadOnlyList<Book>> GetRecommedantiosByOrders(string userId, DateTime? startDate, DateTime? endDate);
        Task<IReadOnlyList<Book>> GetRecommedantionsBestSells();
        Task<IReadOnlyList<Book>> GetRecommedationsRandom();
        Task<List<AuthorWithBookCount>> GetFavoriteAuthors(string userId);
        Task<List<GenreWithBookCount>> GetFavoriteGenres(string userId);
        Task<List<Book>> GetPurchasedBooks(string userId);

    }
}
