using Microsoft.AspNetCore.Identity;
using Route.Talabat.Core.Domain.Contracts.Persistence.DbInitializer;
using Route.Talabat.Core.Domain.Entities.Identity;
using Route.Talabat.Infrastructure.Persistence._Common;

namespace Route.Talabat.Infrastructure.Persistence._Identity
{
    public sealed class StoreIdentityInitializer(StoreIdentityDbContext _dbContext ,UserManager<ApplicationUser> _userManger) : DbInitializer(_dbContext) ,  IStoreIdentityInitializer
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

            await _userManger.CreateAsync(User,"Eslam@2003");
        }
    }
}
