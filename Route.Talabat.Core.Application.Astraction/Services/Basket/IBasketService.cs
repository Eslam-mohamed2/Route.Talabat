using Route.Talabat.Core.Application.Abstraction.Models.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Application.Abstraction.Services.Basket
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetCustomerBasketAsync(string BasketId);
        Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto BasketDto);
        Task DeleteCustomerBasketAsync(string basketId);
    }
}
