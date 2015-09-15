using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp
{
    public interface IExpression
    {
        object Evaluate(IDictionary<string, object> context);
    }
}
