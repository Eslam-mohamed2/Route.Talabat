using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Route.Talaat.APIs.Extensions;
using Route.Talaat.APIs.Services;
using Route.Talaat.Core.Application;
using Route.Talaat.Core.Application.Abstraction;
using Route.Talaat.Infrastructure.Persistence;
using Route.Talabat.APIs.Controllers.Errors;
using Route.Talabat.APIs.Middlewares;
using Route.Talabat.Core.Application.Abstraction.Services.Auth;
using Route.Talabat.Core.Domain.Entities.Identity;
using Route.Talabat.Infrastructure;
using Route.Talabat.Infrastructure.Persistence._Identity;
using System.Text;
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
                        var errors = ActionContext.ModelState.Where(P => P.Value!.Errors.Count() > 0)
                        .Select(P => new ApiValidationErrorResponse.ValidationError() 
                        {
                            Field = P.Key,
                            Errors = P.Value!.Errors.Select(E => E.ErrorMessage)
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
                    var errors = ActionContext.ModelState.Where(P => P.Value!.Errors.Count() > 0).ToList()
                                 .SelectMany(P => P.Value!.Errors)
                                 .Select(E => E.ErrorMessage);
                    return new BadRequestObjectResult(new ApiValidationErrorResponse() { Errors = errors });
                };
            });

            webApplicationBuilder.Services.AddControllers(); // Register the Required Services

            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();

            webApplicationBuilder.Services.AddHttpContextAccessor();
            webApplicationBuilder.Services.AddScoped(typeof(ILoggedInUserService) , typeof(LoggedInUserService));
       

            webApplicationBuilder.Services.AddApplicationServices();
            webApplicationBuilder.Services.AddPersistence(webApplicationBuilder.Configuration);
            webApplicationBuilder.Services.AddInfrastructureServices(webApplicationBuilder.Configuration);

            webApplicationBuilder.Services.AddIdentity<ApplicationUser , IdentityRole>((iIdentityOptions) =>
            {
                iIdentityOptions.SignIn.RequireConfirmedAccount = true;
                iIdentityOptions.SignIn.RequireConfirmedEmail = true;
                iIdentityOptions.SignIn.RequireConfirmedPhoneNumber = true;
                iIdentityOptions.User.RequireUniqueEmail = true;
                iIdentityOptions.Lockout.AllowedForNewUsers = true;
                iIdentityOptions.Lockout.MaxFailedAccessAttempts= 5;
                iIdentityOptions.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromHours(12);
            })
            .AddEntityFrameworkStores<StoreIdentityDbContext>();

            webApplicationBuilder.Services.AddAuthentication(authOptionas =>
            {
                authOptionas.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptionas.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters()
                     {
                         ValidateAudience = true,
                         ValidateIssuer = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ClockSkew = TimeSpan.FromMinutes(0), 
                         ValidAudience = webApplicationBuilder.Configuration["JwtSettings:Audience"],
                         ValidIssuer = webApplicationBuilder.Configuration["JwtSettings:Issuer"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(webApplicationBuilder.Configuration["JwtSettings:Key"]!)),
                     };
                 });

            webApplicationBuilder.Services.Configure<JwtSettings>(webApplicationBuilder.Configuration.GetSection("JwtSettings"));

            #endregion

            var app = webApplicationBuilder.Build();

            #region Databases Initialization

            await app.InitializeDbAsync();
            
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            #endregion

            app.Run(); 
        }
    }
}
