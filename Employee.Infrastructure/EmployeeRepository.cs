using Employee.Domain.Interfaces;
using Employee.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Infrastructure
{
    public class EmployeeRepository : GenericDataAccess, IEmployee
    {
        private readonly ApiSettingsDto globalSettings;

        public EmployeeRepository(ApiSettingsDto settings) : base(settings)
        {
            globalSettings = settings;
        }

        public IEnumerable<EmployeeDto> GetEmployees()
        {
            var employeeDb = GetAsync<EmployeeDto, EmployeeDto>(globalSettings.GetValue("StoredProcedureGetEmployees"), new EmployeeDto(), System.Data.CommandType.StoredProcedure).Result;
            return employeeDb;
        }

        public async Task<bool> InsertEmployee(EmployeeDto employee)
        {
            var employeeDb = await GetAsyncFirst<EmployeeDto, bool>(globalSettings.GetValue("StoredProcedureInsertEmployees"), employee, System.Data.CommandType.StoredProcedure);
            return employeeDb;
        }

        public async Task<bool> UpdateEmployee(EmployeeDto employee)
        {
            var employeeDb = await GetAsyncFirst<EmployeeDto, bool>(globalSettings.GetValue("StoredProcedureEditEmployees"), employee, System.Data.CommandType.StoredProcedure);
            return employeeDb;
        }
    }
}
