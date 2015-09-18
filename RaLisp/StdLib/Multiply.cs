using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    public class Multiply : IFunction
    {
        public string Name
        {
            get
            {
                return "*";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            float runningTotal = 1;
            foreach (float parameter in parameters.Select(x => x.Evaluate(context)))
            {
                runningTotal *= parameter;
            }
            return runningTotal;

        }
    }
}
