using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    public class Print : IFunction
    {
        public string Name
        {
            get
            {
                return "print";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            var text = string.Join(",", parameters.Select(x => x.Evaluate(context).ToString()));
            Console.WriteLine(text);
            return null;
        }
    }
}
