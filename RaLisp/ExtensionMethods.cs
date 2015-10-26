using RaLisp.StdLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp
{
    public static class ExtensionMethods
    {
        public static bool IsTrue(this object value)
        {
            if (value is bool)
            {
                return (bool)value;
            }

            if (value is float)
            {
                return 0 != (float)value;
            }

            if (null == value) return false;

            return true;
        }

        public static object Get(this IDictionary<string,object> value, string key)
        {
            var parts = key.Split('.');
            object ctx = value;
            for (var i = 0; i < parts.Length; i++)
            {
                ctx = (ctx as Dictionary<string,object>)[parts[i]];
            }
            return ctx;
        }

        public static object Set(this IDictionary<string, object> value, string key, object newValue)
        {
            var parts = key.Split('.');
            IDictionary<string,object> ctx = value;
            for (var i = 0; i < parts.Length -1; i++)
            {
                ctx = ctx[parts[i]] as IDictionary<string,object>;
            }
            ctx[parts.Last()] = newValue;
            return newValue;
        }


        public static object Evaluate(this object value, IDictionary<string, object> context)
        {
            // expression        
            if (value is IExpression) return (value as IExpression).Evaluate(context);

            //value
            return value;
        }

        public static IDictionary<string, object> Copy(this IDictionary<string, object> context)
        {
            var result = new Dictionary<string, object>();
            foreach (var key in context)
            {
                result.Set(key.Key, key.Value);
            }
            return result;

        }
    }
}
