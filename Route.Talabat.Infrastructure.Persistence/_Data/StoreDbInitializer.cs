using Route.Talaat.Core.Domain.Entities.Products;
using Route.Talabat.Core.Domain.Contracts.Persistence.DbInitializer;
using Route.Talabat.Infrastructure.Persistence._Common;
using System.Text.Json;

namespace Route.Talaat.Infrastructure.Persistence.Data
{
    public sealed class StoreDbInitializer : DbInitializer, IStoreDbInitializer
    {
        private readonly StoreDbContext _dbContext;

        public StoreDbInitializer(StoreDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task SeedAsync()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Seed Brands
            if (!_dbContext.brands.Any())
            {
                var brandsData = await File.ReadAllTextAsync(Path.Combine(baseDirectory, "Data", "Seeds", "brands.json"));
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count > 0)
                {
                    await _dbContext.Set<ProductBrand>().AddRangeAsync(brands);
                    await _dbContext.SaveChangesAsync();
                }
            }

            // Seed Categories
            if (!_dbContext.categories.Any())
            {
                var categoriesData = await File.ReadAllTextAsync(Path.Combine(baseDirectory, "Data", "Seeds", "categories.json"));
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

                if (categories?.Count > 0)
                {
                    await _dbContext.Set<ProductCategory>().AddRangeAsync(categories);
                    await _dbContext.SaveChangesAsync();
                }
            }

            // Seed Products
            if (!_dbContext.products.Any())
            {
                var productsData = await File.ReadAllTextAsync(Path.Combine(baseDirectory, "Data", "Seeds", "products.json"));
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count > 0)
                {
                    await _dbContext.Set<Product>().AddRangeAsync(products);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }

}
