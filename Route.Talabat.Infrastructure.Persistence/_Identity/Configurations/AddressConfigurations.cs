using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talaat.Infrastructure.Persistence.Data;
using Route.Talabat.Core.Domain.Entities.Identity;
using Route.Talabat.Infrastructure.Persistence._Common;

namespace Route.Talabat.Infrastructure.Persistence._Identity.Configurations
{
    [DbContextType(typeof(StoreIdentityDbContext))]
    internal class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(nameof(Address.Id)).ValueGeneratedOnAdd();
            builder.Property(nameof(Address.FirstName)).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(nameof(Address.LastName)).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(nameof(Address.Street)).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(nameof(Address.City)).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(nameof(Address.Country)).HasColumnType("nvarchar").HasMaxLength(50);

            builder.ToTable("Address");
        }
    }
}
