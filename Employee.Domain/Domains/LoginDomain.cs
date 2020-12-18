using Employee.Domain.Interfaces;
using Employee.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Domains
{
    public class LoginDomain
    {
        private readonly ILogin iLogin;
        private readonly ApiSettingsDto globalSettings;
        public LoginDomain(ILogin loginService, ApiSettingsDto settings)
        {
            iLogin = loginService;
            globalSettings = settings;
        }

        public async Task<Result<bool>> IsValidLogin(LoginModel user)
        {
            var result = new Result<bool>();
            var userdb = await iLogin.IsValidLogin(user);
            if (!userdb)
            {
                return new Result<bool>
                {
                    Data = false,
                    IsSuccess = false,
                    Message = "Usuario o contraseña incorrecta"
                };
            }
            result.Message = "Logueo exitoso";
            result.IsSuccess = true;
            result.Data = userdb;
            return result;
        }
    }
}
