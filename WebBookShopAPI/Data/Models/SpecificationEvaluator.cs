using Microsoft.EntityFrameworkCore;
using WebBookShopAPI.Data.Interfaces;

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

            // order of if's is Important

            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if(spec.IsPagingEnable)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));


            return query;
        }
    }
}
