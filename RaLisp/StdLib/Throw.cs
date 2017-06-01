using System;
using System.Collections.Generic;
using System.Linq;

namespace RaLisp.StdLib
{
    public class Throw : IFunction
    {
        public string Name
        {
            get
            {
                return "throw";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            if (!parameters.Any()) throw new Exception();
            throw new Exception(parameters[0].Evaluate(context).ToString());
        }
    }
}
