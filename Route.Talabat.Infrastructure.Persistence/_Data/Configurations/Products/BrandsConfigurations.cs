using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talaat.Core.Domain.Common;
using Route.Talaat.Core.Domain.Entities.Products;
using Route.Talaat.Infrastructure.Persistence.Data.Configurations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talaat.Infrastructure.Persistence.Data.Configurations.Products
{
    internal class BrandsConfigurations : BaseAuditableEntityConfigurations<ProductBrand ,int>
    {
        public override void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            base.Configure(builder);

            builder.Property(B => B.Name).IsRequired();
        }
    }
}
