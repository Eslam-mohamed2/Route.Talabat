using Route.Talaat.Core.Domain.Common;
using Route.Talaat.Core.Domain.Contracts;
using Route.Talaat.Core.Domain.Entities.Products;
using Route.Talaat.Infrastructure.Persistence.Data;
using Route.Talaat.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talaat.Infrastructure.Persistence.UnitOfWork
{
    internal class UnitOFWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private readonly ConcurrentDictionary<string, object> _repositories;
        public UnitOFWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new ();
        }
   
        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseAuditableEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            ///var TypeName = typeof(TEntity).Name; // Project
            ///if(_repositories.ContainsKey(TypeName)) 
            ///    return (IGenericRepository<TEntity, TKey>)_repositories[TypeName];
            ///
            ///var repository = new GenericRepository<TEntity,TKey>(_dbContext);
            ///_repositories.Add(TypeName, repository);
            ///
            ///return repository;
            ///

            return (IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_dbContext));
        }
    }
}
