using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp
{
    public class Variable : IExpression
    {
        public string Text { get; set; }

      

        public object Evaluate(IDictionary<string, object> context)
        {
            if (this.Text == "true") return true;
            if (this.Text == "false") return false;

            if (this.Text.StartsWith("\"") && this.Text.EndsWith("\"")) return this.Text.Replace("\"", "");
            if (this.Text.StartsWith("'") && this.Text.EndsWith("'")) return this.Text.Replace("'", "");

            float floatValue = 0;
            if (float.TryParse(this.Text, out floatValue))
            {
                return floatValue;
            }

            var value = context.Get(this.Text);
            if (value is IExpression) return (value as IExpression).Evaluate(context);
            return value;
        }
    }
}
