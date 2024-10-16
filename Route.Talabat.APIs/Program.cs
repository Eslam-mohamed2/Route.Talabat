using Route.Talaat.APIs.Extensions;
using Route.Talaat.APIs.Services;
using Route.Talaat.Core.Application.Abstraction;
using Route.Talaat.Infrastructure.Persistence;
using Route.Talaat.Core.Application;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Controllers.Errors;
using Route.Talabat.APIs.Middlewares;
using Route.Talabat.Infrastructure;
using System.Diagnostics;
namespace Route.Talaat.APIs
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

            webApplicationBuilder.Services
                .AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = false;
                    options.InvalidModelStateResponseFactory = (ActionContext) =>
                    {
                        var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                        .Select(P => new ApiValidationErrorResponse.ValidationError() 
                        {
                            Field = P.Key,
                            Errors = P.Value.Errors.Select(E => E.ErrorMessage)
                        });

                        return new BadRequestObjectResult(new ApiValidationErrorResponse()
                        {
                            Errors = (IEnumerable<string>)errors
                        });
                    };
                })
                .AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly);

            webApplicationBuilder.Services.Configure<ApiBehaviorOptions>(options =>
            {
            options.SuppressModelStateInvalidFilter = false;
                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count() > 0).ToList()
                                 .SelectMany(P => P.Value.Errors)
                                 .Select(E => E.ErrorMessage);

                    return new BadRequestObjectResult(new ApiValidationErrorResponse() { Errors = errors });
                };
            });

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

            webApplicationBuilder.Services.AddHttpContextAccessor();
            webApplicationBuilder.Services.AddScoped(typeof(ILoggedInUserService) , typeof(LoggedInUserService));

            webApplicationBuilder.Services.AddApplicationServices();
            webApplicationBuilder.Services.AddInfrastructureServices(webApplicationBuilder.Configuration);
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
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseStatusCodePagesWithReExecute("/Errors/{Code}");

            app.UseStaticFiles();

            app.UseAuthorization();

            app.MapControllers();

            #endregion

            app.Run(); 
        }
    }
}
