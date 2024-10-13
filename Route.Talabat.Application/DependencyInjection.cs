using Microsoft.Extensions.DependencyInjection;
using Route.Talaat.Core.Application.Abstraction.Services;
using Route.Talaat.Core.Application.Abstraction.Services.Products;
using Route.Talaat.Core.Application.Mapping;
using Route.Talaat.Core.Application.Services;
using Route.Talaat.Core.Application.Services.Products;

namespace Route.Talaat.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddAutoMapper(Mapper => Mapper.AddProfile(new MappingProfile()));
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped(typeof(IProductService), typeof (ProductService));

            services.AddScoped<IServiceManger, ServiceManger>();
;           return services;
        }
    }
}
