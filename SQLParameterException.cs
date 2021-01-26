using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager
{
    public class SQLParameterException : Exception
    {
        public SQLParameterException(string message):base(message)
        {
            
        }
    }
}
