using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Domain.Common;
using Route.Talabat.Core.Domain.Contracts;
using Route.Talabat.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistence.Repositories
{
    internal class GenericRepository<TEntity, TKey> (StoreContext _dbContext) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false)
        => WithTracking?  await _dbContext.Set<TEntity>().ToListAsync() : await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
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
