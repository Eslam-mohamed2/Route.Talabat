using Microsoft.EntityFrameworkCore;
using Route.Talaat.Core.Domain.Entities.Products;
using Route.Talabat.Core.Domain.Contracts.Persistence.DbInitializer;
using Route.Talabat.Infrastructure.Persistence._Common;
using System.Text.Json;

namespace Route.Talaat.Infrastructure.Persistence.Data
{
    internal sealed class StoreDbInitializer (StoreDbContext _dbContext) : DbInitializer (_dbContext ), IStoreIdentityDbInitializer
    {

        public override async Task SeedAsync()
        {
            if (!_dbContext.brands.Any())
            {
                var brandsData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistence/Data/Seeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count > 0)
                {
                    await _dbContext.Set<ProductBrand>().AddRangeAsync(brands);
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (!_dbContext.categories.Any())
            {
                var CategoriesData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistence/Data/Seeds/categories.json");
                var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);

                if (Categories?.Count > 0)
                {
                    await _dbContext.Set<ProductCategory>().AddRangeAsync(Categories);
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (!_dbContext.products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistence/Data/Seeds/products.json");
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
