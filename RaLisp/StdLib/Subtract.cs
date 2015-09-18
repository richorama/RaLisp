using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    public class Subtract : IFunction
    {
        public string Name
        {
            get
            {
                return "-";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            if (parameters.Length == 0) return 0;

            float runningTotal = (float) parameters.First().Evaluate(context);
            foreach (float parameter in parameters.Skip(1).Select(x => x.Evaluate(context)))
            {
                runningTotal -= parameter;
            }
            return runningTotal;

        }
    }
}
