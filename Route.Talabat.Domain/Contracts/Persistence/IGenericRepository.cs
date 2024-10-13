using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Contracts.Persistence
{
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false);
        Task<IEnumerable<TEntity>> GetAllWithSpecificationAsync(ISpecifications<TEntity,TKey> spec,bool WithTracking = false);
        Task<TEntity?> GetAsync(TKey id);
        Task<TEntity?> GetWithSpecificationAsync(ISpecifications<TEntity,TKey> spec);
        Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec);

        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
