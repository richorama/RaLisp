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

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            var stringValue = new StringBuilder();
            float floatValue = 0;
            var allFloats = true;
            var allObjects = true;
            var objectValue = new Dictionary<string,object>();

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

                if (value is IDictionary<string, object>)
                {
                    foreach (var kv in value as IDictionary<string, object>)
                    {
                        objectValue.Add(kv.Key, kv.Value);
                    }
                }
                else
                {
                    allObjects = false;
                }

                stringValue.Append(value.ToString());
            }

            if (allFloats)
            {
                return floatValue;
            }
            else if (allObjects)
            {
                return objectValue;
            }
            else
            {
                return stringValue.ToString();
            }
        }

    }

}
