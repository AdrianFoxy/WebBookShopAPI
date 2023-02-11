using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data.Repositories
{
    public interface IGenreRepository
    {
        Task<IReadOnlyList<Genre>> GetAllGenresAsync();
    }
}
