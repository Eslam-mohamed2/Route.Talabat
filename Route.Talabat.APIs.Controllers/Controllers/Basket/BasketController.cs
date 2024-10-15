using Microsoft.AspNetCore.Mvc;
using Route.Talaat.APIs.Controllers.Base;
using Route.Talaat.Core.Application.Abstraction.Services;
using Route.Talabat.Core.Application.Abstraction.Models.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.APIs.Controllers.Controllers.Basket
{
    internal class BasketController(IServiceManger serviceManger) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasket(string Id)
        {
            var basket = await serviceManger.BasketService.GetCustomerBasketAsync(Id);
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basketDto)
        {
            var basket = await serviceManger.BasketService.UpdateCustomerBasketAsync(basketDto);
            return Ok(basket);
        }
        public async Task DeleteBasket(string id)
        {
            await serviceManger.BasketService.DeleteCustomerBasketAsync(id);
        }


    }
}
