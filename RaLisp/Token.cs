using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp
{
    public enum TokenType
    {
        Whitespace,
        OpenBracket,
        CloseBracket,
        Keyword
    }

    public class Token
    {
        public TokenType TokenType { get; set; }

        public string Text { get; set; }
    }
}
