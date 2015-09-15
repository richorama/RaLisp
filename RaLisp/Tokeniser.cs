using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp
{
    public static class Tokeniser
    {
        public static IEnumerable<Token> Tokenise(string input)
        {
            var sb = new StringBuilder();
            var insideString = false;

            for (var i = 0; i < input.Length; i++)
            {
                var character = input[i];

                Token token = null;

                switch (character)
                {
                    case '"':
                    case '\'':
                        insideString = !insideString;
                        sb.Append(character);
                        break;
                    case '\r':
                    case '\n':
                    case ' ':
                        if (!insideString)
                            token = new Token { Text = character.ToString(), TokenType = TokenType.Whitespace };
                        else
                            sb.Append(character);
                        break;
                    case '(':
                        
                        if (!insideString)
                            token = new Token { Text = character.ToString(), TokenType = TokenType.OpenBracket };
                        else
                            sb.Append(character);
                        break;
                    case ')':
                        if (!insideString)
                            token = new Token { Text = character.ToString(), TokenType = TokenType.CloseBracket };
                        else
                            sb.Append(character);
                        break;
                    default:
                        sb.Append(character);
                        break;
                }

                if (token == null)
                {
                    continue;
                }

                if (sb.Length > 0)
                {
                    yield return new Token { Text = sb.ToString(), TokenType = TokenType.Keyword };
                    sb.Clear();
                }

                yield return token;

            }

        }


    }
}
