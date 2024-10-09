using Route.Talabat.Core.Domain.Contracts;

namespace Route.Talabat.APIs.Extensions
{
    public static class InitializerExtensions
    {
        public static async Task<WebApplication> InitializerStoreContextAsync(this WebApplication app)
        {
            using var Scoope = app.Services.CreateAsyncScope();
            var Services = Scoope.ServiceProvider;

            var storeContextInitializer = Services.GetRequiredService<IStoreContextInitializer>();
            // ASking the Runtime Env for an Object from "StoreContext" Service Explicitly.


            var loggerFactory = Services.GetRequiredService<ILoggerFactory>();

            //var logger = Services.GetRequiredService<Logger<Program>>();
            try
            {
                await storeContextInitializer.InitializeAsync();
                await storeContextInitializer.SeedAsync();
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
