
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.Infrastructure.Persistence;
using Route.Talabat.Infrastructure.Persistence.Data;

namespace Route.Talabat.APIs
{
    public class Program
    {
        //[FromServices]
        //public static StoreContext StoreContext { get; set; } = null!;
            
        public static async Task Main(string[] args)
        {

            //StoreContext dbContext = new StoreContext();

            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services

            webApplicationBuilder.Services.AddControllers(); // Register the Required Services
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();

            //webApplicationBuilder.Services.AddDbContext<StoreContext>((Option) =>
            //{
            //    Option.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("StoreContext"));
            //});

            //DependencyInJection.AddPersistence(webApplicationBuilder.Services, webApplicationBuilder.Configuration);

            webApplicationBuilder.Services.AddPersistence( webApplicationBuilder.Configuration);

            #endregion

            var app = webApplicationBuilder.Build();

            #region Update Datebase || (Applky Any Pending Migrations)

            using var Scoope = app.Services.CreateAsyncScope();
            var Services = Scoope.ServiceProvider;
            var dbContext = Services.GetRequiredService<StoreContext>();
            // ASking the Runtime Env for an Object from "StoreContext" Service Explicitly.
            
            
            var loggerFactory = Services.GetRequiredService<ILoggerFactory>();
            
            //var logger = Services.GetRequiredService<Logger<Program>>();
             try
            {
                var PendingMigrations = dbContext.Database.GetPendingMigrations();
                if (PendingMigrations.Any())
            
                    await dbContext.Database.MigrateAsync(); // -> UpdateDatabase
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an Error Has Been Occurd during applying the Migrations.");
            }
            
            #endregion

            #region Configrue Kestrel Middlewares
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            #endregion

            app.Run(); 
        }
    }
}
