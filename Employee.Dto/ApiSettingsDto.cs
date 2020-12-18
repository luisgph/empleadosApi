using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Employee.Dto
{
    public class ApiSettingsDto
    {
        private ApiSettings ListSettings { get; set; }
        public ApiSettingsDto(IOptions<ApiSettings> settings)
        {
            ListSettings = settings.Value;
        }

        public string GetValue(string Key)
        {
            try
            {
                var properties = ListSettings.GetType();
                PropertyInfo value = properties.GetProperty(Key);
                return value.GetValue(ListSettings, null).ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
