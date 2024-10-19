using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Route.Talaat.Core.Application.Abstraction.Services;
using Route.Talaat.Core.Application.Abstraction.Services.Products;
using Route.Talaat.Core.Application.Mapping;
using Route.Talaat.Core.Application.Services;
using Route.Talaat.Core.Application.Services.Products;
using Route.Talabat.Core.Application.Abstraction.Services.Auth;
using Route.Talabat.Core.Application.Abstraction.Services.Basket;
using Route.Talabat.Core.Application.Services.Auth;
using Route.Talabat.Core.Application.Services.Basket;
using Route.Talabat.Core.Domain.Contracts.Infrastructure;
using Route.Talabat.Core.Domain.Entities.Identity;

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

            services.AddScoped(typeof(Func<IBasketService>), (servicesProvider) =>
            {
                var configuration = servicesProvider.GetRequiredService<IConfiguration>();
                var mapper = servicesProvider.GetRequiredService<IMapper>();
                var basketRepository = servicesProvider.GetRequiredService<IBasketRepository>();
                return () => new BasketService(basketRepository, mapper, configuration);

                //return () => servicesProvider.GetRequiredService<IBasketService>();  //This Eay Throw Exception That The IBasketService Wasn't Register
            });

            services.AddScoped(typeof(Func<IAuthService>), serviceProvider =>
            {
                //var auth = serviceProvider.GetRequiredService<IAuthService>();
                var jwtSetting = serviceProvider.GetRequiredService<IOptions<JwtSettings>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var SignInManager = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
                return () => new AuthService(userManager, SignInManager, jwtSetting);
            });


            ; return services;
        }
    }
}
