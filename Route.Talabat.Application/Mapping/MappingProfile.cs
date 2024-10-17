using AutoMapper;
using Route.Talaat.Core.Application.Abstraction.Models.Products;
using Route.Talaat.Core.Domain.Entities.Products;
using Route.Talabat.Core.Application.Abstraction.Models.Basket;
using Route.Talabat.Core.Application.Abstraction.Models.Employees;
using Route.Talabat.Core.Application.Abstraction.Models.Products;
using Route.Talabat.Core.Application.Mapping;
using Route.Talabat.Core.Domain.Entities.Basket;
using Route.Talabat.Core.Domain.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talaat.Core.Application.Mapping
{
    internal class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(D => D.Brand, O => O.MapFrom(src => src.Brand!.Name))
                .ForMember(D => D.Category, O => O.MapFrom(src => src.Category!.Name))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlMapping>());
            CreateMap<ProductBrand, BrandDto>();

            CreateMap<ProductCategory, CategoryDto>();

            CreateMap<Employee, EmployeeToReturnDto>();

            CreateMap<CustomerBasket,CustomerBasketDto>().ReverseMap();

            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
