using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Domain.Contracts;
using Route.Talabat.Core.Domain.Entities.Products;
using System.Text.Json;

namespace Route.Talabat.Infrastructure.Persistence.Data
{
    internal class StoreContextInitializer (StoreContext _dbcontext) : IStoreContextInitializer
    {
        public async Task InitializeAsync()
        {
            var PendingMigrations = _dbcontext.Database.GetPendingMigrations();
               
            if (PendingMigrations.Any())
                await _dbcontext.Database.MigrateAsync();

         }

        public async Task SeedAsync()
        {
            if (!_dbcontext.brands.Any())
            {
                var brandsData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistence/Data/Seeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count > 0)
                {
                    await _dbcontext.Set<ProductBrand>().AddRangeAsync(brands);
                    await _dbcontext.SaveChangesAsync();
                }
            }

            if (!_dbcontext.categories.Any())
            {
                var CategoriesData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistence/Data/Seeds/categories.json");
                var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);

                if (Categories?.Count > 0)
                {
                    await _dbcontext.Set<ProductCategory>().AddRangeAsync(Categories);
                    await _dbcontext.SaveChangesAsync();
                }
            }

            if (!_dbcontext.products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistence/Data/Seeds/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count > 0)
                {
                    await _dbcontext.Set<Product>().AddRangeAsync(products);
                    await _dbcontext.SaveChangesAsync();
                }
            }
        }
    }
}
