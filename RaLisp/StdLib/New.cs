using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    public class New : IFunction
    {
        public string Name
        {
            get
            {
                return "new";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            var result = new Dictionary<string, object>();

            for (var i = 0; i < parameters.Length; i += 2)
            {
                result.Set((parameters[i] as Variable).Name, parameters[i + 1].Evaluate(context));
            }

            return result;
        }
    }
}
