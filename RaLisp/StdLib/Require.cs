using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.StdLib
{
    public class Require : IFunction
    {
        public string Name
        {
            get
            {
                return "require";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            // load a standard lib
            var lib = parameters[0].Evaluate(context).ToString();

            var localFunctions = LoadStandardFunctions(lib);
            if (null != localFunctions)
            {
                var ctx = new Dictionary<string, object>();
                foreach (var func in localFunctions)
                {
                    ctx.Add(func.Name, func);
                }
                return ctx;
            }

            // attempt to load a file
            var code = File.ReadAllText(lib);
            var statement = Parser.Parse(code);
            var moduleContext = Environment.CreateInitialContext();
            moduleContext.Add("export", new Dictionary<string,object>());
            statement.Evaluate(moduleContext);
            return moduleContext.Get("export");
        }

        static IFunction[] LoadStandardFunctions(string ns)
        {
            switch (ns)
            {
                case "io":
                    return Environment.LoadFunctions("RaLisp.IO").ToArray();
                case "json":
                    return Environment.LoadFunctions("RaLisp.Json").ToArray(); 
            }
            return null;
        }


    }
}
