using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Route.Talabat.Core.Domain.Contracts;
using Route.Talabat.Infrastructure.Persistence.Data;
using Route.Talabat.Infrastructure.Persistence.Data.Interceptors;
using Route.Talabat.Infrastructure.Persistence.UnitOfWork;

namespace Route.Talabat.Infrastructure.Persistence
{
    public static  class DependencyInJection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<StoreContext>((OptionsBuilder) =>
            {
                OptionsBuilder.UseSqlServer(Configuration.GetConnectionString("StoreContext"));
            });

            services.AddScoped<IStoreContextInitializer , StoreContextInitializer>();
            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptor));
            //services.AddScoped(typeof(IStoreContextInitializer), typeof(StoreContextInitializer));
            services.AddScoped(typeof(IUnitOfWork) , typeof(UnitOFWork));
            return services;    
        }
    }
}
