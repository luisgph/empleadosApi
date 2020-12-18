using Dapper;
using Employee.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Employee.Infrastructure
{
    public class GenericDataAccess
    {
        private readonly ApiSettingsDto GlobalSettings;
        protected GenericDataAccess(ApiSettingsDto settings) => GlobalSettings = settings;

        public string DBConnectionString { get; set; }
        private IDbConnection Connection()
        {
            return new SqlConnection(GlobalSettings.GetValue("SqlConnection"));
        }

        internal async Task<IEnumerable<XOutput>> GetAsync<TInput, XOutput>(string Name, TInput filter, CommandType command) where TInput : class
        {
            using IDbConnection conn = Connection();
            conn.Open();
            var parameters = filter.GetParameters();
            var result = await conn.QueryAsync<XOutput>(Name, parameters, commandType: command);
            return result;
        }

        internal async Task<XOutput> GetAsyncFirst<TInput, XOutput>(string Name, TInput filter, CommandType command) where TInput : class
        {
            using IDbConnection conn = Connection();
            conn.Open();
            var parameters = filter.GetParameters();
            var result = await conn.QueryFirstOrDefaultAsync<XOutput>(Name, parameters, commandType: command);
            return result;
        }

        internal async Task<IEnumerable<XOutput>> GetAsyncFirstDynamic<XOutput>(string Name, object filter, CommandType command)
        {
            using IDbConnection conn = Connection();
            conn.Open();
            var parameters = filter;
            var result = await conn.QueryAsync<XOutput>(Name, parameters, commandType: command, commandTimeout: 10);
            return result;
        }
    }
}
