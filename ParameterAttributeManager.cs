using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager
{
    public class ParameterAttributeManager
    {
        
        public string GetNameAttributeParameter(PropertyInfo property)
        {
            if (HasAnAttributeParameter(property))
            {
                SQLParameterNameAttribute attr = (SQLParameterNameAttribute)property.GetCustomAttribute(typeof(SQLParameterNameAttribute));
                return attr.ParameterName;
            }
            else
            {
                return property.Name;
            }
        }

        private bool HasAnAttributeParameter(PropertyInfo propertyInfo)
        {
            var attrs = propertyInfo.GetCustomAttributes();
            foreach (var attr in attrs)
            {
                if (attr is SQLParameterNameAttribute) return true;
            }
            return false;
        } 
    }
}
