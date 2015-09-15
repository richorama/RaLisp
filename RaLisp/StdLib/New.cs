using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    public class New : IFunction
    {
        public string Name
        {
            get
            {
                return "new";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            return new Dictionary<string, object>();
        }
    }
}
