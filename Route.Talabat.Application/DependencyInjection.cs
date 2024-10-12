using Microsoft.Extensions.DependencyInjection;
using Route.Talabat.Core.Application.Abstraction.Services.Products;
using Route.Talabat.Core.Application.Mapping;
using Route.Talabat.Core.Application.Services.Products;

namespace Route.Talabat.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddAutoMapper(Mapper => Mapper.AddProfile(new MappingProfile()));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped(typeof(IProductService), typeof(ProductService));
            services.AddScoped(typeof(IProductService), typeof (ProductService));
;           return services;
        }
    }
}
