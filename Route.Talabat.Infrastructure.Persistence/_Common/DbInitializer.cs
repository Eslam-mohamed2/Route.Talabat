using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Domain.Contracts.Persistence.DbInitializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Persistence._Common
{
    internal abstract class DbInitializer(DbContext _dbContext) : IDbInitializer
    {
        public virtual async Task InitializeAsync()
        {
            var PendingMigrations = _dbContext.Database.GetPendingMigrations();

            if (PendingMigrations.Any())
                await _dbContext.Database.MigrateAsync();

        }

        public abstract Task SeedAsync();
        
    }
}
