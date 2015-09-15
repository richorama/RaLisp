using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    public class Not : IFunction
    {
        public string Name
        {
            get
            {
                return "!";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            return !(bool)parameters[0].Evaluate(context);
        }
    }
}
