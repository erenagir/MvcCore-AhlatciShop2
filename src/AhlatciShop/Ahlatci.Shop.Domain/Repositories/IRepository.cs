using Ahlatci.Shop.Domain.Common;
using System.Linq.Expressions;

namespace Ahlatci.Shop.Domain.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetByFilterAsync(Expression<Func<T,bool>> filter);
        Task<T> GetById(object id);
        Task add(T entity);
        Task update(T entity);
        Task delete(object id);
        Task delete(T entity);


    }
}
