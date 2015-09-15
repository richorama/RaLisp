using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp
{
    public static class Parser
    {

        public static Statement Parse(string code)
        {
            return Parse(Tokeniser.Tokenise(code));                
        }

        public static Statement Parse(IEnumerable<Token> tokens)
        {
            var current = new Statement();

            foreach (var token in tokens)
            {
                switch (token.TokenType)
                {
                    case TokenType.OpenBracket:
                        var newStatement = new Statement { Parent = current };
                        current.Expressions.Add(newStatement);
                        current = newStatement;
                        break;

                    case TokenType.CloseBracket:
                        current = current.Parent;
                        break;

                    case TokenType.Keyword:
                        current.Expressions.Add(ParseToken(token.Text));
                        break;
                }                    
            }
            return current;
        }

        static object ParseToken(string value)
        {
            if (value == "true") return true;
            if (value == "false") return false;

            if (value.StartsWith("\"") && value.EndsWith("\"")) return value.Replace("\"", "");
            if (value.StartsWith("'") && value.EndsWith("'")) return value.Replace("'", "");

            float floatValue = 0;
            if (float.TryParse(value, out floatValue))
            {
                return floatValue;
            }

            return new Variable { Name = value };
        }

    }
}
