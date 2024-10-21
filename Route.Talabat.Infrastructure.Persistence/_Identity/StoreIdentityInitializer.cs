using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Route.Talaat.Core.Domain.Entities.Products;
using Route.Talabat.Core.Domain.Contracts.Persistence;
using Route.Talabat.Core.Domain.Contracts.Persistence.DbInitializer;
using Route.Talabat.Core.Domain.Entities.Identity;
using Route.Talabat.Infrastructure.Persistence._Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistence._Identity
{
    internal sealed class StoreIdentityInitializer(StoreIdentityDbContext _dbContext ,UserManager<ApplicationUser> _userManger) : DbInitializer(_dbContext) ,  IStoreIdentityInitializer
    {
        public override async Task SeedAsync()
        {
            var User = new ApplicationUser()
            {
                DisplayName = "Eslam Mohamed",
                UserName = "Eslam.mohamed",
                Email = "eslammohamed@gmail.com",
                PhoneNumber = "01234567890",
            };

            await _userManger.CreateAsync(User);
        }
    }
}
