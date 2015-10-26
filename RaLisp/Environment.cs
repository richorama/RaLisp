using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp
{
    public class Environment
    {
        static IDictionary<string, IFunction[]> cache = new Dictionary<string, IFunction[]>();

        internal static IFunction[] LoadFunctions(string ns)
        {
            if (cache.Keys.Contains(ns)) return cache[ns];

            var type = typeof(IFunction);

            var result = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .Where(p => !p.IsInterface)
                .Where(x => ns == null || x.Namespace == ns)
                .Select(x => Activator.CreateInstance(x) as IFunction).ToArray();

            cache.Add(ns, result);
            return result;
        }

        public static IDictionary<string, object> CreateInitialContext()
        {
            var dictionary = new Dictionary<string, object>();
            foreach (var function in LoadFunctions("RaLisp.StdLib"))
            {
                dictionary.Add(function.Name, function);
            }
            return dictionary;
        }

        public static object Evaluate(string code)
        {
            var ctx = CreateInitialContext();
            var statement = Parser.Parse(Tokeniser.Tokenise(code));
            return statement.Evaluate(ctx);
        }

        public IDictionary<string, object> Context { get; private set; }

        public Environment(params string[] args)
        {
            this.Context = CreateInitialContext();
            this.Context.Add("args", args);
        }

        public object Eval(string code)
        {
            var statement = Parser.Parse(Tokeniser.Tokenise(code));
            return statement.Evaluate(this.Context);
        }


    }
}
