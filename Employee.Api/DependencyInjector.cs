using Employee.Domain.Interfaces;
using Employee.Dto;
using Employee.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Api
{
    public static class DependencyInjector
    {
        public static void UseDependecys(this IServiceCollection services)
        {
            services.AddSingleton<ApiSettingsDto>();
            services.AddScoped<IAuthorization, AuthorizationServices>();
            services.AddScoped<ILogin, LoginRepository>();
            services.AddScoped<IEmployee, EmployeeRepository>();
        }
    }
}
