using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataBaseManager
{
    public class CreatorSqlParameter
    {
        private Dictionary<string, DbType> dbTypes = new Dictionary<string, DbType>()
        {
            {"Int16",DbType.Int16 },
            {"Int32",DbType.Int32 },
            {"Int64",DbType.Int64 },
            {"Double",DbType.Double },
            {"Decimal",DbType.Decimal },
            {"DateTime",DbType.DateTime },
            {"Boolean",DbType.Boolean },
            {"String",DbType.String }
        };

        public IEnumerable<SqlParameter> GetSqlParameters(object entity)
        {
            var properties = entity.GetType().GetProperties();
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            foreach (var property in properties)
            {
                sqlParameters.Add(CreateSqlParameter(entity, property));
            }

            return sqlParameters;
        }

        public SqlParameter CreateSqlParameter(object entity, PropertyInfo propertyInfo)
        {
            ParameterAttributeManager parameterAttributeManager = new ParameterAttributeManager();

            DbType parameterType = GetDbType(propertyInfo.PropertyType);
            string parameterName = parameterAttributeManager.GetNameAttributeParameter(propertyInfo);
            var parameterValue = propertyInfo.GetValue(entity);

            SqlParameter parameter = new SqlParameter()
            {
                DbType = parameterType,
                ParameterName = parameterName,
                Value = parameterValue
            };
            return parameter;
        }

        private DbType GetDbType(Type type)
        {
            DbType dbType;
            if (!dbTypes.TryGetValue(GetTypeParamString(type), out dbType)) throw new SQLParameterException("No se puede convertir ese tipo de dato");
            return dbType;
        }

        private string GetTypeParamString(Type type)
        {
            string[] typeSplit = type.ToString().Split('.');
            string typeString = typeSplit[typeSplit.Length - 1];
            typeString = Regex.Replace(typeString, @"[][\\\/]","");
            return typeString;
        }

    }
}
