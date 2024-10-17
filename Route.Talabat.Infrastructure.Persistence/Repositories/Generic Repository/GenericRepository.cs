using Microsoft.EntityFrameworkCore;
using Route.Talaat.Core.Domain.Common;
using Route.Talaat.Core.Domain.Entities.Products;
using Route.Talaat.Infrastructure.Persistence.Data;
using Route.Talabat.Core.Domain.Contracts;
using Route.Talabat.Core.Domain.Contracts.Persistence;
using Route.Talabat.Infrastructure.Persistence.Repositories.Generic_Repository;

namespace Route.Talaat.Infrastructure.Persistence.Repositories
{
    internal class GenericRepository<TEntity, TKey>(StoreContext _dbContext) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false)
        {
            return WithTracking ? await _dbContext.Set<TEntity>().ToListAsync() :
                    await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllWithSpecificationAsync(ISpecifications<TEntity, TKey> spec, bool WithTracking = false)
        {
            return await SpecificationEvaluator<TEntity, TKey>.GetQuery(_dbContext.Set<TEntity>(), spec).ToListAsync(); 
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity?> GetWithSpecificationAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await SpecificationEvaluator<TEntity,TKey>.GetQuery(_dbContext.Set<TEntity>(), spec).FirstOrDefaultAsync();
        }

        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);

        public  void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

        public async Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await SpecificationEvaluator<TEntity, TKey>.GetQuery(_dbContext.Set<TEntity>(), spec).CountAsync();
        }
    }
}
