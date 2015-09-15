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
                        current.Expressions.Add(new Variable { Text = token.Text });
                        break;
                }                    
            }

            return current;
        }

    }
}
