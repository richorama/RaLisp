using System;
using System.Collections.Generic;
using System.Linq;

namespace RaLisp.StdLib
{
    public class Try : IFunction
    {
        public string Name
        {
            get
            {
                return "try";
            }
        }

        public object Execute(IDictionary<string, object> context, params object[] parameters)
        {
            object returnValue = null;
            if (!parameters.Any()) return returnValue;
            try
            {
                returnValue = parameters[0].Evaluate(context);
            }
            catch (Exception ex)
            {
                if (parameters.Length > 1)
                {
                    context.Set("@", ex);
                    returnValue = parameters[1].Evaluate(context);
                }
            }
            finally
            {
                if (parameters.Length > 2)
                {
                    returnValue = parameters[2].Evaluate(context);

                }
            }
          
            return returnValue;
        }
    }
}
