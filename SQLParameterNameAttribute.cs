using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager
{
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class SQLParameterNameAttribute : System.Attribute
    {
        private string parameterName;

        public SQLParameterNameAttribute(string parameterName)
        {
            this.parameterName = parameterName;
        }

        public string ParameterName
        {
            get { return parameterName; }
        }
    }
}
