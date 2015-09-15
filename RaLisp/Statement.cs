using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp
{


    public class Statement : IExpression
    {
        public Statement()
        {
            this.Expressions = new List<IExpression>();
        }

        public Statement Parent { get; set; }

        public List<IExpression> Expressions { get; set; }

        public object Evaluate(IDictionary<string, object> context)
        {
            if (this.Expressions.Count == 0) return null;

            if (this.Expressions[0] is Variable)
            {
                return (context.Get((this.Expressions[0] as Variable).Text) as IFunction).Execute(context, this.Expressions.Skip(1).ToArray());
            }

            object result = null;
            foreach (Statement statement in this.Expressions)
            {
                result = statement.Evaluate(context);
            }
            return result;

        }

    }


}
