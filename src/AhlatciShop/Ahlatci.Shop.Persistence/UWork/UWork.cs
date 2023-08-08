using Ahlatci.Shop.Domain.Common;
using Ahlatci.Shop.Domain.Repositories;
using Ahlatci.Shop.Domain.UWork;
using Ahlatci.Shop.Persistence.Context;
using Ahlatci.Shop.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Persistence.UWork
{
    public class UWork : IUWork
    {
        private Dictionary<Type, object> _repository;
       
        private readonly AhlatciContext _context;



        public UWork(IServiceProvider serviceProvider, AhlatciContext context)
        {
            _repository = new Dictionary<Type, object>();
           
            _context = context;
        }

        public async Task<bool> ComitAsync()
        {
            var result = false;
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {


                try
                {
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    result= true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                
                    throw;
                }
                
            }
            return result;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            if (_repository.ContainsKey(typeof(IRepository<T>)))
            {
                return (IRepository<T>)_repository[typeof(IRepository<T>)];
            }
            var repository = new Repository<T>(_context);
            _repository.Add(typeof(Repository<T>), repository);

           
            return repository;

        }
    }
}
