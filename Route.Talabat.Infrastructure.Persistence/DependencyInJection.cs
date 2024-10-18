using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Route.Talaat.Infrastructure.Persistence.Data;
using Route.Talaat.Infrastructure.Persistence.Data.Interceptors;
using Route.Talaat.Infrastructure.Persistence.UnitOfWork;
using Route.Talabat.Core.Domain.Contracts.Persistence;
using Route.Talabat.Core.Domain.Contracts.Persistence.DbInitializer;
using Route.Talabat.Core.Domain.Entities.Identity;
using Route.Talabat.Infrastructure.Persistence._Identity;

namespace Route.Talaat.Infrastructure.Persistence
{
    public static  class DependencyInJection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration Configuration)
        {
            #region Store DbContext
            services.AddDbContext<StoreDbContext>((OptionsBuilder) =>
                {
                    OptionsBuilder.UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetConnectionString("StoreContext"));
                });

            services.AddScoped<IStoreIdentityDbInitializer, StoreDbInitializer>();
            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptor));
            //services.AddScoped(typeof(IStoreContextInitializer), typeof(StoreContextInitializer));

            #endregion

            #region Identity Context

            services.AddDbContext<StoreIdentityDbContext>((OptionsBuilder) =>
              {
                  OptionsBuilder.UseLazyLoadingProxies()
                  .UseSqlServer(Configuration.GetConnectionString("IdentityContext"));
              });


            services.AddIdentityCore<ApplicationUser>();


            #endregion
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOFWork));
            return services;

        }
    }
}
