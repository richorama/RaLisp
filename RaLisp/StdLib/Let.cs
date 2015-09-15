using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    public class Let : IFunction
    {
        public string Name
        {
            get
            {
                return "let";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            var value = parameters[1].Evaluate(context);
            context.Set((parameters[0] as Variable).Name, value);
            return value;
        }
    }
}
