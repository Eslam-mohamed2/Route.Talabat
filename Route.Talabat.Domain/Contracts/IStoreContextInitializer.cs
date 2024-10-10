using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Domain.Contracts
{
    public interface IStoreContextInitializer
    {
        Task InitializeAsync();

        Task SeedAsync();
    }
}
