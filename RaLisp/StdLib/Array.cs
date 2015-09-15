using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    public class Array : IFunction
    {
        public string Name
        {
            get
            {
                return "array";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            return parameters.Select(x => x.Evaluate(context)).ToArray();
        }
    }
}
