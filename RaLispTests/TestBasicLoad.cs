using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaLisp;
using RaLisp.StdLib;

namespace RaLispTests
{
    [TestClass]
    public class TestBasicLoad
    {

        [TestMethod]
        public void TestCreateInitialContext()
        {
            var ctx = Startup.CreateInitialContext();
            Assert.IsNotNull(ctx);
            Assert.AreNotEqual(0, ctx.Keys.Count);
            Assert.AreEqual(1, ctx.Keys.Count(x => x == "+"));
        }



    }
}
