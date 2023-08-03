using Ahlatci.Shop.Domain.Common;
using Ahlatci.Shop.Domain.Repositories;
using Ahlatci.Shop.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ahlatci.Shop.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AhlatciContext _context;

        public Repository(AhlatciContext context)
        {
            _context = context;
        }   public async Task<IQueryable<T>> GetAllAsync()
        {
          return await Task.FromResult( _context.Set<T>());
        }

        public async Task<IQueryable<T>> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
           return await Task.FromResult(_context.Set<T>().Where(filter));
        }

        public async Task<T> GetById(object id)
        {
            return await _context.Set<T>().FindAsync(id);

        }


        public async Task add(T entity)
        {
            await    _context.Set<T>().AddAsync(entity);
        }

        public async Task delete(object id)
        {
            var deleted= await _context.Set<T>().FindAsync(id);
           await delete(deleted);    
        }

        public async Task delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            // remove methodunun async olmadığı için async in hata vermemesi için
           await Task.CompletedTask;
        }

     
        public async Task update(T entity)
        {
           _context.Set<T>().Update(entity);
            await Task.CompletedTask;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
          return await _context.Set<T>().AnyAsync(filter);
        }
    }
}
