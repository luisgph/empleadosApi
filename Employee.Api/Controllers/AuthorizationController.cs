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
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly AuthDomain authDomain;

        public AuthorizationController(IAuthorization iAuthRepository, ApiSettingsDto settings)
        {
            authDomain = new AuthDomain(iAuthRepository, settings);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authorization([FromHeader] string ClientId)
        {
            try
            {
                if (!string.IsNullOrEmpty(ClientId))
                {
                    var isValid = await authDomain.IsValidClient(ClientId);
                    if (isValid.IsSuccess)
                    {
                        return Ok(await authDomain.GenerateToken(isValid.Data.ClientID, isValid.Data.SecretKey));

                    }
                    return Unauthorized(isValid);
                }
                return BadRequest();
            }
            catch (System.Exception ex)
            {
                return StatusCode(503, new Result<string> {
                    Data = null,
                    Message = ex.Message,
                    IsSuccess = false
                } );
            }
        }
    }
}
