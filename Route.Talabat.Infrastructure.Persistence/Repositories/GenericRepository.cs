using Microsoft.EntityFrameworkCore;
using Route.Talaat.Core.Domain.Common;
using Route.Talaat.Core.Domain.Contracts;
using Route.Talaat.Core.Domain.Entities.Products;
using Route.Talaat.Infrastructure.Persistence.Data;

namespace Route.Talaat.Infrastructure.Persistence.Repositories
{
    internal class GenericRepository<TEntity, TKey> (StoreContext _dbContext) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false)
        {
            if (typeof(TEntity) == typeof(Product))
                return WithTracking ?
                    (IEnumerable<TEntity>)await _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync() :
                    (IEnumerable<TEntity>)await _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).AsNoTracking().ToListAsync();
            
            return WithTracking?  await _dbContext.Set<TEntity>().ToListAsync() :
                    await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        ///{
        ///    if (WithTracking) return  await _dbContext.Set<TEntity>().ToListAsync();
        ///    return  await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        ///}

        public async Task<TEntity?> GetAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);

        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);

        public async void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)  =>  _dbContext.Set<TEntity>().Update(entity);
    }
}
