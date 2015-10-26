namespace RaLisp.Json
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static partial class Json
    {
        public static string Stringify(object value)
        {
            return SerializeThing(value);
        }

        static string SerializeThing(object value)
        {
            if (value is IEnumerable<object>) return SerializeArray(value as IEnumerable<object>);
            if (value is Array) return SerializeArray(StepThroughArray(value as Array));
            if (value is IDictionary<string,object>) return SerializeDictionary(value as IDictionary<string,object>);
            if (value is string) return string.Format("\"{0}\"", value.ToString());
            if (IsNumber(value)) return value.ToString();
            if (value is bool) return (bool) value ? "true" : "false";
            if (value == null) return "null";
            return SerializeObject(value);
        }

        static IEnumerable<string> SerializeObjectProperties(object value)
        {
            foreach (var prop in value.GetType().GetProperties())
            {
                if (prop.CanRead && prop.GetMethod.IsPublic) yield return string.Format("\"{0}\":{1}", prop.Name, SerializeThing(prop.GetValue(value, null)));
            }
        }

        static IEnumerable<string> SerializeDictionaryProperties(IDictionary<string,object> value)
        {
            foreach (var prop in value.Keys)
            {
                yield return string.Format("\"{0}\":{1}", prop, SerializeThing(value[prop]));
            }
        }

        static string SerializeObject(object value)
        {
            return "{" + string.Join(",", SerializeObjectProperties(value)) + "}";
        }

        static string SerializeDictionary(IDictionary<string,object> value)
        {
            return "{" + string.Join(",", SerializeDictionaryProperties(value)) + "}";
        }

        static string SerializeArray(IEnumerable<object> values)
        {
            return "[" + string.Join(",", values.Select(x => SerializeThing(x))) + "]";
        }

        static IEnumerable<object> StepThroughArray(Array values)
        {
            for (var i = 0; i < values.Length; i++) yield return values.GetValue(i);
        }


        static bool IsNumber(this object value)
        {
            return value is sbyte
                || value is byte
                || value is short
                || value is ushort
                || value is int
                || value is uint
                || value is long
                || value is ulong
                || value is float
                || value is double
                || value is decimal;
        }


    }
}
