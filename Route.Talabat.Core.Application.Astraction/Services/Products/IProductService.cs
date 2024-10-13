using Route.Talaat.Core.Application.Abstraction.Models.Products;
using Route.Talabat.Core.Application.Abstraction.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talaat.Core.Application.Abstraction.Services.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductToReturnDto>> GetProductsAsync(ProductSpecificationParams specParams);
        Task<ProductToReturnDto> GetProductAsync(int id);
        Task<IEnumerable<BrandDto>> GetBrandsAsync();
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
}
