using AutoMapper;
using Route.Talabat.Core.Application.Abstraction.Models.Employees;
using Route.Talabat.Core.Application.Abstraction.Services.Employees;
using Route.Talabat.Core.Domain.Contracts.Persistence;
using Route.Talabat.Core.Domain.Entities.Employees;
using Route.Talabat.Core.Domain.Specifications.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Application.Services.Employees
{
    internal class EmployeeService(IUnitOfWork unitOfWork,IMapper mapper) : IEmployeeService
    {
        public async Task<EmployeeToReturnDto> GetEmployeeAsync(int id)
        {
            var spec = new EmployeeWithDepartmentSpecifications(id);

            var employees = await unitOfWork.GetRepository<Employee, int>().GetWithSpecificationAsync(spec);

            var employeeToReturn = mapper.Map<EmployeeToReturnDto>(employees);

            return employeeToReturn;
        }

        public async Task<IEnumerable<EmployeeToReturnDto>> GetEmployeesAsync()
        {
            var spec = new EmployeeWithDepartmentSpecifications();

            var employees = await unitOfWork.GetRepository<Employee, int>().GetAllWithSpecificationAsync(spec);

            var employeeToReturn = mapper.Map<IEnumerable<EmployeeToReturnDto>>(employees);

            return employeeToReturn;
        }
    }
}
