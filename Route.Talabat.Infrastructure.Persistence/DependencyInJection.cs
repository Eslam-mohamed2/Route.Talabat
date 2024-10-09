using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Route.Talabat.Core.Domain.Contracts;
using Route.Talabat.Infrastructure.Persistence.Data;

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

            //services.AddScoped(typeof(IStoreContextInitializer), typeof(StoreContextInitializer));

            return services;    
        }
    }
}
