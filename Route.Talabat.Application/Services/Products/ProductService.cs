using AutoMapper;
using Route.Talaat.Core.Application.Abstraction.Models.Products;
using Route.Talaat.Core.Application.Abstraction.Services.Products;
using Route.Talaat.Core.Domain.Contracts;
using Route.Talaat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Route.Talabat.Core.Application.Abstraction.Services;

namespace Route.Talaat.Core.Application.Services.Products
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync()
        {
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync();

            var ProductsToReturn = mapper.Map<IEnumerable<ProductToReturnDto>>(products);

            return ProductsToReturn;

        }

        public async Task<IEnumerable<ProductToReturnDto>> GetProductAsync(int id)
        {
            var Product = await unitOfWork.GetRepository<Product, int>().GetAsync(id);

            var ProductsToReturn = mapper.Map<IEnumerable<ProductToReturnDto>>(Product);

            return ProductsToReturn;
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var Brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var BrandToReturn = mapper.Map<IEnumerable<BrandDto>>(Brands);
            return BrandToReturn;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var Category = await unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();
            var CategoryToReturn = mapper.Map<IEnumerable<CategoryDto>>(Category);
            return CategoryToReturn;
        }
    }
}
