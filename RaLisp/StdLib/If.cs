using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    public class If : IFunction
    {
        public string Name
        {
            get
            {
                return "?";
            }
        }

        public object Execute(IDictionary<string, object> context, params IExpression[] parameters)
        {
            if (parameters[0].Evaluate(context).IsTrue())
            {
                return parameters[1].Evaluate(context);
            }
            if (parameters.Length > 2)
            {
                return parameters[2].Evaluate(context);
            }
            return null;
        }
    }
}
