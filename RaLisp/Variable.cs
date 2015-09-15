using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp
{
    public class Variable : IExpression
    {
        public string Name { get; set; }

        public object Evaluate(IDictionary<string, object> context)
        {
            var value = context.Get(this.Name);
            if (value is IExpression) return (value as IExpression).Evaluate(context);
            return value;
        }
    }
}
