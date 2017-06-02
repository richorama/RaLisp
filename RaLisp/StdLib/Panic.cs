using System.Collections.Generic;
using System.Linq;

namespace RaLisp.StdLib
{
    public class Panic : IFunction
    {
        public string Name
        {
            get
            {
                return "panic";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            var exitCode = -1;
            if (parameters.Any())
            {
                var value = parameters[0].Evaluate(context);
                int.TryParse(value.ToString(), out exitCode);
            }
            System.Environment.Exit(exitCode);
            return null;
        }
    }
}
