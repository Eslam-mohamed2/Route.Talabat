using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Route.Talaat.Infrastructure.Persistence.Data;
using Route.Talaat.Infrastructure.Persistence.Data.Interceptors;
using Route.Talaat.Infrastructure.Persistence.UnitOfWork;
using Route.Talabat.Core.Domain.Contracts.Persistence;

namespace Route.Talaat.Infrastructure.Persistence
{
    public static  class DependencyInJection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<StoreContext>((OptionsBuilder) =>
            {
                OptionsBuilder.UseLazyLoadingProxies()
                .UseSqlServer(Configuration.GetConnectionString("StoreContext"));
            });

            services.AddScoped<IStoreContextInitializer , StoreContextInitializer>();
            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptor));
            //services.AddScoped(typeof(IStoreContextInitializer), typeof(StoreContextInitializer));
            services.AddScoped(typeof(IUnitOfWork) , typeof(UnitOFWork));
            return services;    
        }
    }
}
