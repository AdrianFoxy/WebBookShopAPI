using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetBookByIdAsync(int id);
        Task<IReadOnlyList<Book>> GetAllBooksCatalogAsync();

    }
}
