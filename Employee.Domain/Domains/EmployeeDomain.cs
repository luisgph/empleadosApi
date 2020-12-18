using Employee.Domain.Interfaces;
using Employee.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Employee.Domain.Domains
{
    public class EmployeeDomain
    {
        private readonly IEmployee iEmployee;
        private readonly ApiSettingsDto globalSettings;
        public EmployeeDomain(IEmployee employeeService, ApiSettingsDto settings)
        {
            iEmployee = employeeService;
            globalSettings = settings;
        }

        public async Task<Result<List<EmployeeDto>>> GetEmployees()
        {
            var result = new Result<List<EmployeeDto>> { Data = new List<EmployeeDto>() };
            var employeesdb = iEmployee.GetEmployees();
            if (employeesdb != null)
            {
                result.IsSuccess = true;
                result.Data = employeesdb.ToList();
                result.Message = "Consulta exitosa";
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "No se fue posible consultar los empleados";
            }

            return result;
        }

        public async Task<Result<bool>> UpdateEmployee(EmployeeDto employee)
        {
            var result = new Result<bool>();
            var employeedb = await iEmployee.UpdateEmployee(employee);
            if (!employeedb)
            {
                result.IsSuccess = true;
                result.Data = employeedb;
                result.Message = "El empleado se modfico con éxito";
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "No fue posible modicar el empleado";
            }
            return result;
        }

        public async Task<Result<bool>> InsertEmployee(EmployeeDto employee)
        {
            var result = new Result<bool>();
            var employeedb = await iEmployee.InsertEmployee(employee);
            if (!employeedb)
            {
                result.IsSuccess = true;
                result.Data = employeedb;
                result.Message = "El empleado fue creado con éxito";
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "No fue posible crear el empleado";
            }

            return result;
        }
    }
}
