using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talabat.Core.Domain.Entities.Products;
using Route.Talabat.Infrastructure.Persistence.Data.Configurations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistence.Data.Configurations.Products
{
    internal class CategoryConfigurations : BaseEntityConfigurations<ProductCategory, int>
    {
        public override void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            base.Configure(builder);
            builder.Property(C => C.Name).IsRequired();
        }
    }
}
