using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLispTests
{
    [TestClass]
    public class TestIo
    {
        [TestMethod]
        public void TestOpen()
        {
            RaLisp.Environment.Evaluate(@"
                (fn err text => print text)
                (io-open 'testopen.txt' @)");

        }


        [TestMethod]
        public void TestOpenLambda()
        {
            RaLisp.Environment.Evaluate(@"
                (io-open 'testopen.txt' (fn err text => print text))");

        }
    }
}
