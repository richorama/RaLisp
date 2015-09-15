using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    public class Map : IFunction
    {
        public string Name
        {
            get
            {
                return "map";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            return (parameters[0].Evaluate(context) as object[])
                .Select(x => (parameters[1].Evaluate(context) as IFunction).Execute(context, new object[] { x })).ToArray();
        }
    }
}
