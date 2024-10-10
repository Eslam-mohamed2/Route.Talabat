
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.APIs.Extensions;
using Route.Talabat.Core.Domain.Contracts;
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

            webApplicationBuilder.Services.AddPersistence(webApplicationBuilder.Configuration);

            #endregion

            var app = webApplicationBuilder.Build();

            #region Databases Initialization

            await app.InitializerStoreContextAsync();
            
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
