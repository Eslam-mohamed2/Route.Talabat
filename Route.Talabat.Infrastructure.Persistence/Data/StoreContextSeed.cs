using Route.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistence.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync (StoreContext dbContext)
        {
            if (!dbContext.brands.Any())
            {
                var brandsData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistence/Data/Seeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count > 0)
                {
                    await dbContext.Set<ProductBrand>().AddRangeAsync(brands);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.categories.Any())
            {
                var CategoriesData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistence/Data/Seeds/categories.json");
                    var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);

                if (Categories?.Count > 0)
                {
                   await dbContext.Set<ProductCategory>().AddRangeAsync(Categories);
                   await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../Route.Talabat.Infrastructure.Persistence/Data/Seeds/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count > 0)
                {
                    await dbContext.Set<Product>().AddRangeAsync(products);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
