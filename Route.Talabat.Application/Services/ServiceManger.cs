using AutoMapper;
using Route.Talabat.Core.Application.Abstraction.Services;
using Route.Talabat.Core.Application.Abstraction.Services.Products;
using Route.Talabat.Core.Application.Services.Products;
using Route.Talabat.Core.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Application.Services
{
    internal class ServiceManger : IServiceManger
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly Lazy<IProductService> _productService;

        public ServiceManger(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork,_mapper));
        }

        public IProductService ProductService => _productService.Value;
       
    }
}
