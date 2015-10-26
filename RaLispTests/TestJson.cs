using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLispTests
{
    [TestClass]
    public class TestJson
    {
        [TestMethod]
        public void TestJsonParse()
        {
            var json = RaLisp.Environment.Evaluate("(let js (require 'json')) (js.parse '{ \"foo\" : 123 }')") as IDictionary<string,object>;
            Assert.IsNotNull(json);
            Assert.AreEqual((double)123, json["foo"]);
        }

        [TestMethod]
        public void TestJsonStringify()
        {
            var json = RaLisp.Environment.Evaluate(@"(let js (require 'json')) (new foo 123) (js.stringify @)") as string;
            Assert.IsNotNull(json);
            Assert.AreEqual("{\"foo\":123}", json);
        }

    }
}
