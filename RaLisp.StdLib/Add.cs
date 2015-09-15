using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaLisp;

namespace RaLisp.StdLib
{
    public class Add : IFunction
    {
        public string Name { get { return "+"; } }

        public object Execute(IDictionary<string, object> context, params IExpression[] parameters)
        {
            var stringValue = new StringBuilder();
            float floatValue = 0;
            var allFloats = true;

            foreach (var item in parameters)
            {
                var value = item.Evaluate(context);
                if (value is float)
                {
                    floatValue += (float)value;
                }
                else
                {
                    allFloats = false;
                }
                stringValue.Append(value.ToString());
            }

            if (allFloats)
            {
                return floatValue;
            }
            else
            {
                return stringValue.ToString();
            }
        }

    }

}
