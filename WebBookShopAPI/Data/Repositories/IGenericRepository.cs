using WebBookShopAPI.Data.Models;
using WebBookShopAPI.Data.Specifications;

namespace WebBookShopAPI.Data.Repositories
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    }
}
