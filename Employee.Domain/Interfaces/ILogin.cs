using Employee.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Interfaces
{
    public interface ILogin
    {
        Task<bool> IsValidLogin(LoginModel user);
    }
}
