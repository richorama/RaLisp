using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp
{
    public interface IFunction
    {
        object Execute(IDictionary<string, object> context, params object[] parameters);

        string Name { get; }
    }
}
