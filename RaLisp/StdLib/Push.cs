using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    public class Push : IFunction
    {
        public string Name
        {
            get
            {
                return "push";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            var array = parameters[0].Evaluate(context) as object[];
            var list = new List<object>(array);
            list.Add(parameters[1].Evaluate(context));
            return list.ToArray();
        }
    }
}
