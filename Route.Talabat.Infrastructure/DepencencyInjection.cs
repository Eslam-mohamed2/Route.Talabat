using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Route.Talabat.Core.Domain.Contracts.Infrastructure;
using StackExchange.Redis;

namespace Route.Talabat.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(typeof(IConnectionMultiplexer), (serviceProvider) =>
            {
                var connectionString = configuration.GetConnectionString("Redis");
                var ConnectionMultiplexerObj = ConnectionMultiplexer.Connect(connectionString!);
                return ConnectionMultiplexerObj;
            });

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository.BasketRepository));
            return services;
        }
    }
}
