using Route.Talabat.Core.Application.Abstraction.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Application.Abstraction.Services
{
    public interface IServiceManger
    {
        public IProductService ProductService { get; }
    }
}
