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
            var evaledParams = parameters.Select(x => x.Evaluate(context)).ToArray();

            for (var i = 0; i < evaledParams.Length -1; i++)
            {
                if (!evaledParams[i].Equals(evaledParams[i+1])) return false;
            }
            return true;
        }
    }
}
