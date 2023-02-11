using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data.Repositories
{
    public interface IBookRepository
    {
        Task<Book> GetBookByIdAsync(int id);
        Task<IReadOnlyList<Book>> GetAllBooksCatalogAsync();

    }
}
