using AutoMapper;
using Route.Talaat.Core.Application.Abstraction.Models.Products;
using Route.Talaat.Core.Application.Abstraction.Services.Products;
using Route.Talaat.Core.Domain.Entities.Products;
using Route.Talabat.Core.Application.Abstraction.Common;
using Route.Talabat.Core.Application.Abstraction.Models.Products;
using Route.Talabat.Core.Domain.Contracts.Persistence;
using Route.Talabat.Core.Domain.Specifications.Products;
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
        public async Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecificationParams specParams)
        {
            var spec = new IncludingBrandAndCategory(specParams.Sort, specParams.BrandId, specParams.CategoryId, specParams.pageSize, specParams.pageIndex, specParams.Search);

            var products = await unitOfWork.GetRepository<Product, int>().GetAllWithSpecificationAsync(spec);

            var data = mapper.Map<IEnumerable<ProductToReturnDto>>(products);
            var CountSpec = new ProductWithFillterationForCountSpecifitions(specParams.BrandId, specParams.CategoryId, specParams.Search);
            var Count = await unitOfWork.GetRepository<Product, int>().GetCountAsync(CountSpec);

            return new Pagination<ProductToReturnDto>(specParams.pageIndex,specParams.pageSize, Count) { Data = data};
        }

            public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var spec = new IncludingBrandAndCategory(id);
            var Product = await unitOfWork.GetRepository<Product, int>().GetWithSpecificationAsync(spec);

            var ProductsToReturn = mapper.Map<ProductToReturnDto>(Product);

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
