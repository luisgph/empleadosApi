using Employee.Domain.Interfaces;
using Employee.Dto;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Infrastructure
{
    public class AuthorizationServices : IAuthorization
    {
        private readonly ApiSettingsDto globalSettings;
        public AuthorizationServices(ApiSettingsDto settings)
        {
            globalSettings = settings;
        }
        public Task<JwtDto> GenerateToken(string clientid, string secretkey)
        {

            var responseToken = new JwtDto();
            var expiresToken = DateTime.Now.AddHours(3);
            var header = new JwtHeader(
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey)),
                    SecurityAlgorithms.HmacSha256)
                );

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, clientid)
            };
            var payload = new JwtPayload(
                issuer: globalSettings.GetValue("Issuer"),
                audience: globalSettings.GetValue("Audience"),
                claims: claims,
                notBefore: DateTime.Now,
                expires: expiresToken
            );

            var token = new JwtSecurityToken(header, payload);
            responseToken.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
            responseToken.Expires = expiresToken;

            return Task.FromResult(responseToken);
        }

        public Task<Credentials> GetCredentials()
        {
            Credentials credential = new Credentials();

            var clientIdAuth = globalSettings.GetValue("ClientIdAuth");
            if (!string.IsNullOrEmpty(clientIdAuth))
            {
                credential.ClientID = clientIdAuth;
                credential.SecretKey = globalSettings.GetValue("SecretKeyAuth");
            }

            return Task.FromResult(credential);
        }
    }
}
