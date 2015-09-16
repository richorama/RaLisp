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
            this.Expressions = new List<object>();
        }

        public Statement Parent { get; set; }

        public List<object> Expressions { get; set; }

        public object Evaluate(IDictionary<string, object> context)
        {
            if (this.Expressions.Count == 0) return null;

            if (this.Expressions[0] is Variable)
            {
                // this is a function
                var output = (context.Get((this.Expressions[0] as Variable).Name) as IFunction).Execute(context, this.Expressions.Skip(1).ToArray());
                context.Set(">", output);
                return output;
            }

            // this is a number of lines of code
            object result = null;
            foreach (Statement statement in this.Expressions)
            {
                result = statement.Evaluate(context);
            }

            context.Set(">", result);

            return result;

        }

    }


}
