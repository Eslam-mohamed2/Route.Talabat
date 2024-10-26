using AutoMapper;
using Route.Talaat.Core.Domain.Entities.Products;
using Route.Talabat.Dashboard.Models;

namespace Route.Talabat.Dashboard.Helpers
{
    public class MapsProfile : Profile
    {
        public MapsProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.ProductCategory, opt => opt.MapFrom(src => src.Category))
                .ReverseMap();
            CreateMap<ProductBrand, ProductBrandViewModel>()
                .ReverseMap();
        }
    }
}
