using Employee.Domain.Interfaces;
using Employee.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Infrastructure
{
    public class LoginRepository : GenericDataAccess, ILogin
    {
        private readonly ApiSettingsDto globalSettings;

        public LoginRepository(ApiSettingsDto settings) : base(settings)
        {
            globalSettings = settings;
        }

        public async Task<bool> IsValidLogin(LoginModel user)
        {
            var userDb = await GetAsyncFirst<LoginModel, bool>(globalSettings.GetValue("StoredProcedureLogin"), user, System.Data.CommandType.StoredProcedure);
            return userDb;
        }
    }
}
