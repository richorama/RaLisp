using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    public class Range : IFunction
    {
        public string Name
        {
            get
            {
                return "range";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            if (parameters.Length == 1) return Enumerable.Range(0, Convert.ToInt32(parameters[0].Evaluate(context))).Select(x => (double)x).ToArray();
            return Enumerable.Range(Convert.ToInt32(parameters[0].Evaluate(context)), Convert.ToInt32(parameters[1].Evaluate(context))).Select(x => (double)x).ToArray();
        }
    }
}
