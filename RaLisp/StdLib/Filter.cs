using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    public class Filter : IFunction
    {
        public string Name
        {
            get
            {
                return "filter";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            var ctxCopy = context.Copy();
            return (parameters[0].Evaluate(context) as object[])
                .Where(x => (bool) ((parameters[1].Evaluate(ctxCopy) as IFunction).Execute(context, new object[] { x }))).ToArray();
        }
    }
}
