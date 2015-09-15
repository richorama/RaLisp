using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaLisp;

namespace RaLispTests
{
    [TestClass]
    public class TestParser
    {
        [TestMethod]
        public void TestTokensier()
        {
            var tokens = Tokeniser.Tokenise("(print \"te st\")").ToArray();
            Assert.AreEqual(5, tokens.Length);
            Assert.AreEqual(TokenType.OpenBracket, tokens[0].TokenType);
            Assert.AreEqual(TokenType.Keyword, tokens[1].TokenType);
            Assert.AreEqual(TokenType.Whitespace, tokens[2].TokenType);
            Assert.AreEqual(TokenType.Keyword, tokens[3].TokenType);
            Assert.AreEqual(TokenType.CloseBracket, tokens[4].TokenType);

            Assert.AreEqual("print", tokens[1].Text);
            Assert.AreEqual("\"te st\"", tokens[3].Text);
        }

        [TestMethod]
        public void TestTokeniserAndParser()
        {
            var statement = Parser.Parse(Tokeniser.Tokenise("(print \"te st\") (foo (bar))"));

            Assert.AreEqual(2, statement.Expressions.Count);
            Assert.AreEqual("print", (((statement.Expressions[0] as Statement).Expressions[0]) as Variable).Text);
        }

        [TestMethod]
        public void TestTokeniserAndParserWithLinebreak()
        {
            var code = @"
(print 'te st')
  (foo (bar))";
            var statement = Parser.Parse(Tokeniser.Tokenise(code));

            Assert.AreEqual(2, statement.Expressions.Count);
            Assert.AreEqual("print", (((statement.Expressions[0] as Statement).Expressions[0]) as Variable).Text);
        }

    }
}
