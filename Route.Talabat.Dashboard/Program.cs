using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Route.Talaat.Infrastructure.Persistence.Data;
using Route.Talabat.Core.Domain.Contracts.Persistence;
using Route.Talabat.Core.Domain.Entities.Identity;
using Route.Talabat.Dashboard.Helpers;
using Route.Talabat.Infrastructure.Persistence._Identity;
using Route.Talaat.Infrastructure.Persistence.UnitOfWork;
namespace Route.Talaat.Dashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region ConfigureServices
            // Add services to the container.

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<StoreDbContext>(optionsBuilder =>
            {
                optionsBuilder
                .UseSqlServer(builder.Configuration.GetConnectionString("StoreContext"));
            });

            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOFWork));
            builder.Services.AddAutoMapper(typeof(MapsProfile));

            builder.Services.AddDbContext<StoreIdentityDbContext>(optionsBuilder =>
            {
                optionsBuilder
                .UseSqlServer(builder.Configuration.GetConnectionString("IdentityContext"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(identityOptions =>
            {
                identityOptions.User.RequireUniqueEmail = true;

                identityOptions.Lockout.AllowedForNewUsers = true;
                identityOptions.Lockout.MaxFailedAccessAttempts = 5;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(12);

            })
                .AddEntityFrameworkStores<StoreIdentityDbContext>();

            #endregion

            var app = builder.Build();

            #region Configure Kestrell Middlewares
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Login}/{id?}");
            #endregion

            app.Run();
        }
    }
}
