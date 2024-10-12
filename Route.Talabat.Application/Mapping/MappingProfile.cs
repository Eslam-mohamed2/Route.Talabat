using AutoMapper;
using Route.Talabat.Core.Application.Abstraction.Models.Products;
using Route.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Application.Mapping
{
    internal class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(D => D.Brand, O => O.MapFrom(src => src.Brand!.Name))
                .ForMember(D => D.Category, O => O.MapFrom(src => src.Category!.Name));

            CreateMap<ProductBrand, BrandDto>();

            CreateMap<ProductCategory, CategoryDto>();
        }
    }
}
