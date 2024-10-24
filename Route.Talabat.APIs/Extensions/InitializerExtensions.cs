using Route.Talabat.Core.Domain.Contracts.Persistence.DbInitializer;

namespace Route.Talaat.APIs.Extensions
{
    public static class InitializerExtensions
    {
        public static async Task<WebApplication> InitializeDbAsync(this WebApplication app)
        {
            using var Scope = app.Services.CreateAsyncScope();

            var Services = Scope.ServiceProvider;

            var storeDbInitializer = Services.GetRequiredService<IStoreDbInitializer>();

            var StoreIdentityDbInitializer = Services.GetRequiredService<IStoreIdentityInitializer>();

            var loggerFactory = Services.GetRequiredService<ILoggerFactory>();

            try
            {
                await storeDbInitializer.InitializeAsync();
                await storeDbInitializer.SeedAsync();

                await StoreIdentityDbInitializer.InitializeAsync();
                await StoreIdentityDbInitializer.SeedAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an Error Has Been Occurd during applying the Migrations or the data seeding.");
            }

            return app;
        }
 
    }
}
