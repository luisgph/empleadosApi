using Employee.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Interfaces
{
    public interface IAuthorization
    {
        Task<JwtDto> GenerateToken(string clientid, string secretkey);
        Task<Credentials> GetCredentials();
    }
}
