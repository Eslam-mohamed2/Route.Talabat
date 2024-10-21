using Route.Talabat.Core.Domain.Contracts.Persistence.DbInitializer;

namespace Route.Talaat.APIs.Extensions
{
    public static class InitializerExtensions
    {
        public static async Task<WebApplication> InitializeDbAsync(this WebApplication app)
        {
            using var Scoope = app.Services.CreateAsyncScope();
            var Services = Scoope.ServiceProvider;

            var storeDbInitializer = Services.GetRequiredService<IStoreIdentityDbInitializer>();
            var StoreIdentityDbInitializer = Services.GetRequiredService<IStoreIdentityDbInitializer>();

            // ASking the Runtime Env for an Object from "StoreContext" Service Explicitly.


            var loggerFactory = Services.GetRequiredService<ILoggerFactory>();

            //var logger = Services.GetRequiredService<Logger<Program>>();
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
