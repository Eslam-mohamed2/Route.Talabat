using AutoMapper;
using Microsoft.Extensions.Configuration;
using Route.Talaat.Core.Application.Abstraction.Models.Products;
using Route.Talaat.Core.Domain.Entities.Products;
using Route.Talabat.Core.Application.Abstraction.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Route.Talabat.Core.Application.Mapping
{
    internal class ProductPictureUrlMapping(IConfiguration configuration) : IValueResolver<Product, ProductToReturnDto, string>
    {
        //private readonly IConfiguration _configuration = configuration;

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{configuration["Urls:ApiBaseUrl"]}/{source.PictureUrl}";
            }
            return string.Empty; 
        }
    }
}
