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
    public class LoginController : ControllerBase
    {
        private readonly LoginDomain loginDomain;

        public LoginController(ILogin loginRepository, ApiSettingsDto settings)
        {
            loginDomain = new LoginDomain(loginRepository, settings);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel userLogin)
        {
            try
            {
                if (userLogin != null)
                {
                    var user = await loginDomain.IsValidLogin(userLogin);
                    return Ok(user);
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
