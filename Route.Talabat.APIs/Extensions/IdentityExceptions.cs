using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Route.Talabat.Core.Application.Abstraction.Services.Auth;
using Route.Talabat.Core.Domain.Entities.Identity;
using Route.Talabat.Infrastructure.Persistence._Identity;
using System.Text;

namespace Route.Talabat.APIs.Extensions
{
    public static class IdentityExceptions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(identityOptions =>
            {
                identityOptions.User.RequireUniqueEmail = true;

                identityOptions.Lockout.AllowedForNewUsers = true;
                identityOptions.Lockout.MaxFailedAccessAttempts = 5;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(12);
            })
            .AddEntityFrameworkStores<StoreIdentityDbContext>();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidAudience = configuration["JwtSettings:Audience"],
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            ///services.AddAuthentication((authencicationOptions) =>
            ///{
            ///    authencicationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            ///}).AddJwtBearer((Options) =>
            ///{
            ///    Options.TokenValidationParameters = new TokenValidationParameters()
            ///    {
            ///        ValidateAudience = true,
            ///        ValidateIssuer = true,
            ///        ValidateLifetime = true,
            ///        ValidateIssuerSigningKey = true,
            ///        ValidAudience = configuration["JwtSettings:Audience"],
            ///        ValidIssuer = configuration["JwtSettings:Issuer"],
            ///        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"])),
            ///        ClockSkew = TimeSpan.Zero
            ///    };
            ///});
            ///services.AddScoped(typeof(IAuthService), typeof(AuthService));
            ///services.AddScoped(typeof(Func<IAuthService>), (ServiceProvider) =>
            ///{
            ///    return () =>  ServiceProvider.GetRequiredService<IAuthService>();
            ///});

            return services;
        }
    }
}
