using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Route.Talabat.Infrastructure.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return services;    
        }
    }
}
