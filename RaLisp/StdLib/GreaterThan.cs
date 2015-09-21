using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    public class GreaterThan : IFunction
    {
        public string Name
        {
            get
            {
                return ">";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            var first = true;
            float lastValue = 0;
            foreach (float value in parameters.Select(x => x.Evaluate(context)))
            {
                if (first)
                {
                    lastValue = value;
                    first = false;
                    continue;
                }

                if (value <= lastValue) return false;
            }
            return true;
        }
    }
}
