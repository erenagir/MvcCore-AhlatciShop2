using Ahlatci.Shop.Domain.Common;
using Ahlatci.Shop.Domain.Repositories;
using Ahlatci.Shop.Persistence.Context;
using System.Linq.Expressions;

namespace Ahlatci.Shop.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AhlatciContext _context;

        public Repository(AhlatciContext context)
        {
            _context = context;
        }

        public async Task add(T entity)
        {
            await    _context.Set<T>().AddAsync(entity);
        }

        public Task delete(object id)
        {
          var deleted=_context.Set<T>().Find(id);

        }

        public async Task delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            // remove methodunun async olmadığı için async in hata vermemesi için
           await Task.CompletedTask;
        }

        public Task<List<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
