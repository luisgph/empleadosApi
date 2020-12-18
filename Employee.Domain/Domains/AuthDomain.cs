using Employee.Domain.Interfaces;
using Employee.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Domains
{
    public class AuthDomain
    {
        private readonly IAuthorization iAuth;
        private readonly ApiSettingsDto globalSettings;
        public AuthDomain(IAuthorization authRepository, ApiSettingsDto settings)
        {
            iAuth = authRepository;
            globalSettings = settings;
        }

        public async Task<Result<Credentials>> IsValidClient(string clientId)
        {
            var credential = await iAuth.GetCredentials();
            var result = new Result<Credentials>
            {
                Message = "Credenciales no validas",
                IsSuccess = false
            };

            if (clientId.Equals(globalSettings.GetValue("ClientIdAuth")))
            {
                result.Message ="Credencial valida";
                result.IsSuccess = true;
                result.Data = credential;
                return result;
            }
            return result;
        }

        public async Task<Result<JwtDto>> GenerateToken(string clientId, string secretKey)
        {
            Result<JwtDto> result = new Result<JwtDto>();
            var token = await iAuth.GenerateToken(clientId, secretKey);
            result.IsSuccess = true;
            result.Message = "Token generado exitosamente";
            result.Data = token;
            return result;
        }
    }
}
