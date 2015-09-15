using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaLisp;

namespace RaLispTests
{
    [TestClass]
    public class TestStdLib
    {
        [TestMethod]
        public void TestAdd()
        {
            Assert.AreEqual((float)3, Startup.Evaluate("(+ 1 2)"));
            Assert.AreEqual("12", Startup.Evaluate("(+ 1 '2')"));
            Assert.AreEqual("12", Startup.Evaluate("(+ '1' '2')"));
        }

        [TestMethod]
        public void TestAnd()
        {
            Assert.IsFalse((bool)Startup.Evaluate("(& true false)"));
            Assert.IsTrue((bool)Startup.Evaluate("(& true true)"));
            Assert.IsTrue((bool)Startup.Evaluate("(& true 'hello')"));
            Assert.IsTrue((bool)Startup.Evaluate("(& true 'hello' 1)"));
            Assert.IsFalse((bool)Startup.Evaluate("(& true 'hello' 0)"));
        }

        [TestMethod]
        public void TestOr()
        {
            Assert.IsTrue((bool)Startup.Evaluate("(| true false)"));
            Assert.IsTrue((bool)Startup.Evaluate("(| true true)"));
            Assert.IsFalse((bool)Startup.Evaluate("(| false false)"));
            Assert.IsTrue((bool)Startup.Evaluate("(| true 'hello')"));
            Assert.IsTrue((bool)Startup.Evaluate("(| true 'hello' 1)"));
            Assert.IsFalse((bool)Startup.Evaluate("(| false 0)"));
        }

        [TestMethod]
        public void TestIf()
        {
            Assert.AreEqual("yes", Startup.Evaluate("(? true 'yes')"));
            Assert.AreEqual(null, Startup.Evaluate("(? false 'yes')"));
            Assert.AreEqual((float) 2, Startup.Evaluate("(? false 'yes' 2)"));
            Assert.AreEqual("hello", Startup.Evaluate("(? true (+ 'he' 'llo') 2)"));
        }

        [TestMethod]
        public void TestLet()
        {
            Assert.AreEqual("hello world", Startup.Evaluate("(let foo 'hello') (+ foo ' ' 'world')"));

        }

        [TestMethod]
        public void TestFn()
        {
            Assert.AreEqual("hello world", Startup.Evaluate(@"
                (let foo 
                    (fn a => + 'hello' ' ' a))
                (foo 'world')
                "));
        }


        [TestMethod]
        public void TestPrint()
        {
            Startup.Evaluate("(print 'hello world')");
        }

        [TestMethod]
        public void TestNew()
        {
            Assert.IsTrue(Startup.Evaluate("(new)") is IDictionary<string, object>);
            Assert.AreEqual("hello world", Startup.Evaluate("(let x (new)) (let x.foo 'world') (let qux x.foo) (+ 'hello' ' ' qux)"));
        }


        [TestMethod]
        public void TestRequire()
        {
            Assert.AreEqual("hello", Startup.Evaluate("(let x (require 'dep.txt')) (x)"));
            Assert.AreEqual("hello foo", Startup.Evaluate("(let x (require 'dep2.txt')) (x.foo)"));
        }

        [TestMethod]
        public void TestArray()
        {
            var array = Startup.Evaluate("(array 'foo' 'bar')") as object[];
            Assert.IsNotNull(array);
            Assert.AreEqual(2, array.Length);
            Assert.AreEqual("foo", array[0]);
            Assert.AreEqual("bar", array[1]);
        }

        [TestMethod]
        public void TestPush()
        {
            var array = Startup.Evaluate("(push (array 'foo') 'bar')") as object[];
            Assert.IsNotNull(array);
            Assert.AreEqual(2, array.Length);
            Assert.AreEqual("foo", array[0]);
            Assert.AreEqual("bar", array[1]);
        }


        [TestMethod]
        public void TestFunctionsCallingFunctions()
        {
            Assert.AreEqual((float) 6,  Startup.Evaluate("(+ (+ 1 1) (+ 2 2))"));
        }

    }


}
