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

            if (parameters.Length == 0) return null;

            var evaluatedParams = parameters.Select(x => x.Evaluate(context)).ToArray();

            if (evaluatedParams.All(x => x is float))
            {
                return evaluatedParams.Sum(x => (float)x);
            }

            if (evaluatedParams.All(x => x is IDictionary<string, object>))
            {
                var objectValue = new Dictionary<string, object>();
                foreach (IDictionary<string,object> value in evaluatedParams)
                foreach (var kv in value as IDictionary<string, object>)
                {
                    objectValue.Add(kv.Key, kv.Value);
                }
                return objectValue;
            }

            if (evaluatedParams.All(x => x is object[]))
            {
                var output = new List<object>();
                foreach (object[] value in evaluatedParams)
                {
                    output.AddRange(value);
                }
                return output.ToArray();
            }

            var stringValue = new StringBuilder();
            foreach (var item in evaluatedParams)
            {
                stringValue.Append(item.ToString());
            }
            return stringValue.ToString();

  
        }

    }

}
