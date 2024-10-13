using Microsoft.AspNetCore.Mvc;
using Route.Talaat.APIs.Controllers.Base;
using Route.Talaat.Core.Application.Abstraction.Models.Products;
using Route.Talaat.Core.Application.Abstraction.Services;
using Route.Talabat.Core.Application.Abstraction.Common;
using Route.Talabat.Core.Application.Abstraction.Models.Products;

namespace Route.Talabat.APIs.Controllers.Controllers.Products
{
    public class ProductsController(IServiceManger serviceManger) : BaseApiController
    {
        [HttpGet] // Get: /api/Products
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecificationParams specParams )
        {
            var products = await serviceManger.ProductService.GetProductsAsync(specParams);
            return Ok(products);
        }

        [HttpGet("{id:int}")] // Get: /api/Products/id
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProduct(int id) 
        { 
            var product = await serviceManger.ProductService.GetProductAsync(id);
            if (product == null) 
                return NotFound(new {statusCode = 404 , message = "not Found"});
            return Ok(product);
        }

        [HttpGet("Brands")] // Get: /api/Products/brands
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var brands = await serviceManger.ProductService.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("Categories")] // Get: /api/Products/brands
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await serviceManger.ProductService.GetCategoriesAsync();
            return Ok(categories);
        }
            
    }
}
