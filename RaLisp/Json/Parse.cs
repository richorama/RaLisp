using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.Json
{
    public class Parse : IFunction
    {
        public string Name
        {
            get
            {
                return "parse";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            return Json.Parse(parameters[0].Evaluate(context).ToString());
        }
    }
}
