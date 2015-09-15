using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    public class Or : IFunction
    {
        public string Name
        {
            get
            {
                return "|";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            foreach (var item in parameters)
            {
                var value = item.Evaluate(context);
                if (value.IsTrue()) return true;
            }
            return false;
        }
    }
}
