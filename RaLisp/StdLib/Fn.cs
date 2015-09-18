using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    public class Fn : IFunction
    {
        public string Name
        {
            get
            {
                return "fn";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            var args = parameters.TakeWhile(x => (x as Variable).Name != "=>").Select(x => (x as Variable).Name).ToArray();
            var body = parameters.Skip(args.Length + 1).ToArray();
            return new UserFunction(args, body);
        }

        class UserFunction : IFunction
        {
            public UserFunction()
            { }

            public UserFunction(string[] args, object[] body)
            {
                this.Args = args;
                this.Expressions = body;
            }

            public string[] Args { get; private set; }

            public object[] Expressions { get; private set; }

            public string Name
            {
                get
                {
                    return "user-defined-function";
                }
            }

            public object Execute(IDictionary<string, object> context, params object[] parameters)
            {
                var newContext = new Dictionary<string, object>();
                foreach (var item in context)
                {
                    newContext.Add(item.Key, item.Value);
                }
                for (var i = 0; i < this.Args.Length; i++)
                {
                    newContext.Add(this.Args[0], parameters[i]);
                }


                if (this.Expressions.Length > 1)
                {
                    var output = (new Statement { Expressions = this.Expressions.ToList() }).Evaluate(newContext);
                    context.Set("@", output);
                    return output;
                }

                var output2 = this.Expressions[0].Evaluate(newContext);
                context.Set("@", output2);
                return output2;

            }
        }
    }
}
