using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Route.Talaat.Core.Application.Abstraction.Services;
using Route.Talaat.Core.Application.Abstraction.Services.Products;
using Route.Talaat.Core.Application.Mapping;
using Route.Talaat.Core.Application.Services;
using Route.Talaat.Core.Application.Services.Products;
using Route.Talabat.Core.Application.Abstraction.Services.Basket;
using Route.Talabat.Core.Application.Services.Basket;
using Route.Talabat.Core.Domain.Contracts.Infrastructure;

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

            //services.AddScoped(typeof(IBasketService), typeof(BasketService));
            //services.AddScoped(typeof(Func<IBasketService>),typeof(Func<BasketService>))

            services.AddScoped(typeof(Func<IBasketService>), (ServiceProvider) =>
            {
                var mapper = ServiceProvider.GetRequiredService<IMapper>();
                var Configuration = ServiceProvider.GetRequiredService<IConfiguration>();
                var basketRepository = ServiceProvider.GetRequiredService<IBasketRepository>();
                return new BasketService(basketRepository,mapper, Configuration);   
            });
;           return services;
        }
    }
}
