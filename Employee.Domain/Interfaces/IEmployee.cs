using Employee.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Interfaces
{
    public interface IEmployee
    {
        IEnumerable<EmployeeDto> GetEmployees();
        Task<bool> UpdateEmployee(EmployeeDto employee);
        Task<bool> InsertEmployee(EmployeeDto employee);
    }
}
