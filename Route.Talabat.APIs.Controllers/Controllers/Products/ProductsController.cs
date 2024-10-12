using Microsoft.AspNetCore.Mvc;
using Route.Talaat.APIs.Controllers.Base;
using Route.Talaat.Core.Application.Abstraction.Models.Products;
using Route.Talaat.Core.Application.Abstraction.Services;

namespace Route.Talabat.APIs.Controllers.Controllers.Products
{
    public class ProductsController(IServiceManger serviceManger) : BaseApiController
    {
        [HttpGet] // Get/api/Controller
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProduct()
        {
            var products = await serviceManger.ProductService.GetProductsAsync();
            return Ok(products);
        }

    }
}
