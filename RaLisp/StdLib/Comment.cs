using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    class Comment : IFunction
    {
        public string Name
        {
            get
            {
                return "comment";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            return null;
        }
    }
}
