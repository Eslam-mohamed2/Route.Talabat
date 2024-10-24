using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talaat.Core.Domain.Common;
using Route.Talabat.Infrastructure.Persistence._Common;
using Route.Talabat.Infrastructure.Persistence._Identity;

namespace Route.Talaat.Infrastructure.Persistence.Data.Configurations.Base
{
    [DbContextType(typeof(StoreIdentityDbContext))]
    internal class BaseEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseAuditableEntity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(E => E.Id).ValueGeneratedOnAdd();
        }
    }
}
