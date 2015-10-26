namespace RaLisp.Json
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Text;

    public static partial class Json
    {
        public enum TokenType
        {
            StartArray,
            EndArray,
            StartObj,
            EndObj,
            String,
            Separator,
            Assignment,
            Value
        }

        struct Token
        {
            public TokenType Type { get; set; }
            public string Value { get; set; }
        }

        public static object Parse(string json)
        {
            var stack = new Queue<Token>(Tokenize(json));
            return GetTheNextThing(stack);
        }

        static object GetTheNextThing(Queue<Token> stack)
        {
            var nextObj = stack.Peek();
            switch (nextObj.Type)
            {
                case TokenType.StartObj:
                    return ParseObject(stack);
                case TokenType.String:
                    return stack.Dequeue().Value;
                case TokenType.StartArray:
                    return ParseArray(stack);
                case TokenType.Value:
                    if (nextObj.Value == "null") return null;
                    else if (nextObj.Value == "true") return true;
                    else if (nextObj.Value == "false") return false;
                    else return double.Parse(nextObj.Value);
                default:
                    throw new FormatException(string.Format("unexepected token {0}", nextObj.Value));
            }
        }


        static IDictionary<string, object> ParseObject(Queue<Token> stack)
        {
            var first = stack.Dequeue();
            if (first.Type != TokenType.StartObj) throw new FormatException("expected {");

            var thisObject = new ExpandoObject() as IDictionary<string, object>;

            while (stack.Count > 0)
            {
                var nextToken = stack.Dequeue();
                if (nextToken.Type == TokenType.EndObj) break;
                if (nextToken.Type == TokenType.Separator) continue;
                if (nextToken.Type == TokenType.String)
                {
                    var propertyName = nextToken.Value;
                    if (stack.Dequeue().Type != TokenType.Assignment) throw new FormatException("expected ':'");
                    thisObject.Add(propertyName, GetTheNextThing(stack));
                }
            }

            return thisObject;
        }

 

        static IList<object> ParseArray(Queue<Token> stack)
        {
            var first = stack.Dequeue();
            if (first.Type != TokenType.StartArray) throw new FormatException("expected [");

            var thisArray = new List<object>();

            while (stack.Count > 0)
            {
                var nextToken = stack.Peek();
                if (nextToken.Type == TokenType.EndArray)
                {
                    stack.Dequeue();
                    break;
                }
                if (nextToken.Type == TokenType.Separator)
                {
                    stack.Dequeue();
                    continue;
                }
                thisArray.Add(GetTheNextThing(stack));
            }

            return thisArray;
        }


        static IEnumerable<Token> Tokenize(string json)
        {
            var builder = new StringBuilder();
            var inString = false;
            var escape = false;
            for (var i = 0; i < json.Length; i++)
            {
                var c = json[i];
                if (inString) {

                    if (escape)
                    {
                        if (c == '"') builder.Append(c);
                        if (c == 'r') builder.Append("\r");
                        if (c == 'n') builder.Append("\n");
                        if (c == 't') builder.Append("\t");
                        if (c == 'b') builder.Append("\b");
                        if (c == 'f') builder.Append("\f");
                        if (c == 'f') builder.Append("\f");
                        if (c == '/') builder.Append("/");
                        escape = false;
                        continue;
                    }
                    if (c == '"')
                    {
                        yield return new Token { Type = TokenType.String, Value = builder.ToString() };
                        builder.Clear();
                        inString = false;
                        continue;
                    }
                    if (c == '\\')
                    {
                        escape = true;
                        continue;
                    }
                    escape = false;
                    builder.Append(c);
                    continue;
                }

                switch (c)
                {
                    case '{':
                        yield return new Token { Type = TokenType.StartObj };
                        continue;
                    case '}':
                        if (builder.Length > 0) yield return new Token { Type = TokenType.Value, Value = builder.ToString() };
                        yield return new Token { Type = TokenType.EndObj };
                        continue;
                    case '[':
                        yield return new Token { Type = TokenType.StartArray };
                        continue;
                    case ']':
                        if (builder.Length > 0) yield return new Token { Type = TokenType.Value, Value = builder.ToString() };
                        yield return new Token { Type = TokenType.EndArray };
                        continue;
                    case ' ':
                    case '\r':
                    case '\n':
                        continue;
                    case ',':
                        if (builder.Length > 0) yield return new Token { Type = TokenType.Value, Value = builder.ToString()};
                        yield return new Token { Type = TokenType.Separator };
                        builder.Clear();
                        continue;
                    case '"':
                        inString = true;
                        continue;
                    case ':':
                        yield return new Token { Type = TokenType.Assignment };
                        continue;
                    default:
                        builder.Append(c);
                        continue;
                }

            }

            // JSON could just be a single 'true' for example
            if (builder.Length > 0) yield return new Token { Type = TokenType.Value, Value = builder.ToString() };
        }
    }
}
