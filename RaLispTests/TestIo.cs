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
                (let io (require 'io'))
                (fn err text => print text)
                (io.open '..\..\..\testopen.txt' @)");

        }


        [TestMethod]
        public void TestOpenLambda()
        {
            RaLisp.Environment.Evaluate(@"
                (let io (require 'io'))
                (io.open '..\..\..\testopen2.txt' (fn err text => print text))");

        }
    }
}
