using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data.Interfaces
{
    public interface IGenreRepository
    {
        Task<IReadOnlyList<Genre>> GetAllGenresAsync();
    }
}
