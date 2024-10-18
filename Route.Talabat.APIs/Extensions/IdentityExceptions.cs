using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
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
                ///identityOptions.SignIn.RequireConfirmedAccount = true;
                ///identityOptions.SignIn.RequireConfirmedEmail = true;
                ///identityOptions.SignIn.RequireConfirmedPhoneNumber = true;

                ///identityOptions.Password.RequireNonAlphanumeric = true;
                ///identityOptions.Password.RequiredUniqueChars = 2;
                ///identityOptions.Password.RequiredLength = 6;
                ///identityOptions.Password.RequireDigit = true;
                ///identityOptions.Password.RequireLowercase = true;
                ///identityOptions.Password.RequireUppercase = true;

                identityOptions.User.RequireUniqueEmail = true;
                //identityOptions.User.AllowedUserNameCharacters = "/[a-zA-Z0-9]/";

                identityOptions.Lockout.AllowedForNewUsers = true;
                identityOptions.Lockout.MaxFailedAccessAttempts = 5;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(12);

                //identityOptions.Stores
                //identityOptions.Tokens
                //identityOptions.ClaimsIdentity
            })
            .AddEntityFrameworkStores<StoreIdentityDbContext>();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddAuthentication(authOptionas =>
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

                        ValidAudience = configuration["JwtSettings:Audience"],
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}
