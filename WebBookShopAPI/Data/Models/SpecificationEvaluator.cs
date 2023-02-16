using Microsoft.EntityFrameworkCore;
using WebBookShopAPI.Data.Specifications;

namespace WebBookShopAPI.Data.Models
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        // Обьяснялка как работает, если забудешь:
        // Получаем inputQuery, например books. Далее в ифе проверяем наличие критерия, например это может быть b => b.PublisherId == id
        // потом формируется окончательный результат на этом этапе и возвращается
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
