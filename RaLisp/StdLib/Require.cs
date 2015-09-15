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
            var code = File.ReadAllText(parameters[0].Evaluate(context).ToString());
            var statement = Parser.Parse(code);
            var moduleContext = Startup.CreateInitialContext();
            moduleContext.Add("export", new Dictionary<string,object>());
            statement.Evaluate(moduleContext);
            return moduleContext.Get("export");
        }
    }
}
