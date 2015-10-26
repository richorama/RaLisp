using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.IO
{
    public class Open : IFunction
    {
        public string Name
        {
            get
            {
                return "io-open";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            var file = new FileStream(parameters[0].Evaluate(context).ToString(), FileMode.Open);
            var reader = new StreamReader(file);
            var task = reader.ReadToEndAsync();
            var newContext = context.Copy();
            task.ContinueWith(x =>
            {
                if (parameters.Length > 1) (parameters[1].Evaluate(newContext) as IFunction).Execute(newContext, x.Exception, x.Result);
                reader.Dispose();
                file.Dispose();
            });
            return null;
        }
    }
}
