using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    class Equals : IFunction
    {
        public string Name
        {
            get
            {
                return "=";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            for (var i = 0; i < parameters.Length -1; i++)
            {
                if (!parameters[i].Equals(parameters[i+1])) return false;
            }
            return true;
        }
    }
}
