using AutoMapper;
using Microsoft.Extensions.Configuration;
using Route.Talaat.Core.Application.Abstraction.Services;
using Route.Talaat.Core.Application.Abstraction.Services.Products;
using Route.Talaat.Core.Application.Services.Products;
using Route.Talabat.Core.Application.Abstraction.Services.Auth;
using Route.Talabat.Core.Application.Abstraction.Services.Basket;
using Route.Talabat.Core.Application.Abstraction.Services.Employees;
using Route.Talabat.Core.Application.Services.Basket;
using Route.Talabat.Core.Application.Services.Employees;
using Route.Talabat.Core.Domain.Contracts.Infrastructure;
using Route.Talabat.Core.Domain.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talaat.Core.Application.Services
{
    internal class ServiceManger : IServiceManger
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthService> _AuthService;

        public ServiceManger(IUnitOfWork unitOfWork, IMapper mapper,IConfiguration configuration,Func<IBasketService> basketServiceFactory,Func<IAuthService> AuthServiceFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(_unitOfWork, _mapper));
            _basketService = new Lazy<IBasketService>(basketServiceFactory,LazyThreadSafetyMode.ExecutionAndPublication);
            _AuthService = new Lazy<IAuthService>(AuthServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public IProductService ProductService => _productService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthService AuthService => _AuthService.Value;
    }
}
