using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talabat.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistence.Data.Configurations.Base
{
    internal class BaseEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseAuditableEntity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(E => E.Id).ValueGeneratedOnAdd();
        }
    }
}
