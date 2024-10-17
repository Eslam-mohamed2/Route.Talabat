using AutoMapper;
using Microsoft.Extensions.Configuration;
using Route.Talabat.Core.Application.Abstraction.Models.Basket;
using Route.Talabat.Core.Application.Abstraction.Services.Basket;
using Route.Talabat.Core.Domain.Contracts.Infrastructure;
using Route.Talabat.Core.Domain.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Application.Services.Basket
{
    internal class BasketService(IBasketRepository basketRepository, IMapper mapper,IConfiguration configuration) : IBasketService
    {
        public async Task DeleteCustomerBasketAsync(string basketId)
        {
            var deleted = await basketRepository.DeleteAsync(basketId);

            if (!deleted) throw new Exception();
        }

        public async Task<CustomerBasketDto> GetCustomerBasketAsync(string BasketId)
        {
            var basket = await basketRepository.GetAsync(BasketId);

            if(basket is null) throw new Exception();

            return mapper.Map<CustomerBasketDto>(basket);
        }

        public async Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto BasketDto)
        {
            var Basket = mapper.Map<CustomerBasket>(BasketDto);

            var timeToLive = TimeSpan.FromDays(double.Parse(configuration.GetSection("configuration")["TimeToLiveInDays"]!));

            var updatedBasket = await basketRepository.UpdateAsync(Basket, timeToLive);
            if (updatedBasket is null) throw new Exception();

            return BasketDto;
        }
    }
}
