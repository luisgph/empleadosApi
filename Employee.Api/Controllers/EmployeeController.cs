using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee.Domain.Domains;
using Employee.Domain.Interfaces;
using Employee.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDomain employeeDomain;

        public EmployeeController(IEmployee employeeRepository, ApiSettingsDto settings)
        {
            employeeDomain = new EmployeeDomain(employeeRepository, settings);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            try
            {
                var employeeList = await employeeDomain.GetEmployees();
                return Ok(employeeList);
            }
            catch (Exception ex)
            {
                return StatusCode(503, new Result<string>
                {
                    Data = null,
                    Message = ex.Message,
                    IsSuccess = false
                });
            }
        }

        [HttpPost]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeDto employeeDto)
        {
            try
            {
                if (!string.IsNullOrEmpty(employeeDto.EmployeeId.ToString()) &&
                    !string.IsNullOrEmpty(employeeDto.Name) &&
                    !string.IsNullOrEmpty(employeeDto.LastName) &&
                    !string.IsNullOrEmpty(employeeDto.Role))
                {
                    var employeeUpdate = await employeeDomain.UpdateEmployee(employeeDto);
                    return Ok(employeeUpdate);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(503, new Result<string>
                {
                    Data = null,
                    Message = ex.Message,
                    IsSuccess = false
                });
            }
        }

        [HttpPost]
        [Route("InsertEmployee")]
        public async Task<IActionResult> InsertEmployee(EmployeeDto employeeDto)
        {
            try
            {
                if (!string.IsNullOrEmpty(employeeDto.Name) &&
                    !string.IsNullOrEmpty(employeeDto.LastName) &&
                    !string.IsNullOrEmpty(employeeDto.Role) &&
                    !string.IsNullOrEmpty(employeeDto.NumberId))
                {
                    var employeeUpdate = await employeeDomain.InsertEmployee(employeeDto);
                    return Ok(employeeUpdate);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(503, new Result<string>
                {
                    Data = null,
                    Message = ex.Message,
                    IsSuccess = false
                });
            }
        }
    }
}
