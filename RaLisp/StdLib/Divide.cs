using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    public class Divide : IFunction
    {
        public string Name
        {
            get
            {
                return "/";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            if (parameters.Length != 2) throw new ArgumentException("expected 2 arguments");

            var evaled = parameters.Select(x => (float) x.Evaluate(context)).ToArray();
            return evaled[0] / evaled[1];
        }
    }
}
