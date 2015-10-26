using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.IO
{
    public class Stingify : IFunction
    {
        public string Name
        {
            get
            {
                return "json-stringify";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            return Json.Json.Stringify(parameters[0].Evaluate(context));
        }
    }
}
