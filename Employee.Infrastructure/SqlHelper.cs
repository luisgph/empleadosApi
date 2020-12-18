using Dapper;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Employee.Infrastructure
{
    public static class SqlHelper
    {
        public static DynamicParameters GetParameters<T>(this T obj) where T : class
        {
            var propertyInfos = obj.GetType().GetRuntimeProperties();
            var parameters = new DynamicParameters();
            foreach (var property in propertyInfos)
            {
                var value = property.GetValue(obj);
                var name = property.Name;
                if (value == null || (Object.ReferenceEquals(value.GetType(), typeof(string)) &&
                    string.IsNullOrWhiteSpace(value.ToString())) ||
                    value.IsDefaultValue())
                    continue;
                parameters.Add(name, value);
            }

            return parameters;

        }

        private static bool IsDefaultValue(this object value)
        {
            return value switch
            {
                int i => default == i,
                DateTime i => default == i,
                decimal i => default == i,
                double i => default == i,
                float i => default == i,
                long i => default == i,
                short i => default == i,
                uint i => default == i,
                ulong i => default == i,
                ushort i => default == i,
                sbyte i => default == i,
                Guid i => default == i,
                _ => false,
            };
        }
    }
}
