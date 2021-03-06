﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RaLispTests
{
    [TestClass]
    public class TestStdLib
    {
        [TestMethod]
        public void TestAdd()
        {
            Assert.AreEqual((float)3, RaLisp.Environment.Evaluate("(+ 1 2)"));
            Assert.AreEqual("12", RaLisp.Environment.Evaluate("(+ 1 '2')"));
            Assert.AreEqual("12", RaLisp.Environment.Evaluate("(+ '1' '2')"));
        }

        [TestMethod]
        public void TestAnd()
        {
            Assert.IsFalse((bool)RaLisp.Environment.Evaluate("(& true false)"));
            Assert.IsTrue((bool)RaLisp.Environment.Evaluate("(& true true)"));
            Assert.IsTrue((bool)RaLisp.Environment.Evaluate("(& true 'hello')"));
            Assert.IsTrue((bool)RaLisp.Environment.Evaluate("(& true 'hello' 1)"));
            Assert.IsFalse((bool)RaLisp.Environment.Evaluate("(& true 'hello' 0)"));
        }

        [TestMethod]
        public void TestOr()
        {
            Assert.IsTrue((bool)RaLisp.Environment.Evaluate("(| true false)"));
            Assert.IsTrue((bool)RaLisp.Environment.Evaluate("(| true true)"));
            Assert.IsFalse((bool)RaLisp.Environment.Evaluate("(| false false)"));
            Assert.IsTrue((bool)RaLisp.Environment.Evaluate("(| true 'hello')"));
            Assert.IsTrue((bool)RaLisp.Environment.Evaluate("(| true 'hello' 1)"));
            Assert.IsFalse((bool)RaLisp.Environment.Evaluate("(| false 0)"));
        }

        [TestMethod]
        public void TestIf()
        {
            Assert.AreEqual("yes", RaLisp.Environment.Evaluate("(? true 'yes')"));
            Assert.AreEqual(null, RaLisp.Environment.Evaluate("(? false 'yes')"));
            Assert.AreEqual((float) 2, RaLisp.Environment.Evaluate("(? false 'yes' 2)"));
            Assert.AreEqual("hello", RaLisp.Environment.Evaluate("(? true (+ 'he' 'llo') 2)"));
        }

        [TestMethod]
        public void TestLet()
        {
            Assert.AreEqual("hello world", RaLisp.Environment.Evaluate("(let foo 'hello') (+ foo ' ' 'world')"));

        }

        [TestMethod]
        public void TestFn()
        {
            Assert.AreEqual("hello world", RaLisp.Environment.Evaluate(@"
                (let foo (fn a => + 'hello' ' ' a))
                (foo 'world')
                "));
        }


        [TestMethod]
        public void TestPrint()
        {
            RaLisp.Environment.Evaluate("(print 'hello world')");
        }

        [TestMethod]
        public void TestNew()
        {
            Assert.IsTrue(RaLisp.Environment.Evaluate("(new)") is IDictionary<string, object>);
            Assert.AreEqual("hello world", RaLisp.Environment.Evaluate("(let x (new)) (let x.foo 'world') (let qux x.foo) (+ 'hello' ' ' qux)"));

            Assert.AreEqual("hello world", RaLisp.Environment.Evaluate("(let x (new foo 'hello world')) (let y (fn => x.foo)) (y)"));
        }


        [TestMethod]
        public void TestRequire()
        {
            Assert.AreEqual("hello", RaLisp.Environment.Evaluate(@"(let x (require '..\..\..\dep.txt')) (x)"));
            Assert.AreEqual("hello foo", RaLisp.Environment.Evaluate(@"(let x (require '..\..\..\dep2.txt')) (x.foo)"));
        }

        [TestMethod]
        public void TestArray()
        {
            var array = RaLisp.Environment.Evaluate("(array 'foo' 'bar')") as object[];
            Assert.IsNotNull(array);
            Assert.AreEqual(2, array.Length);
            Assert.AreEqual("foo", array[0]);
            Assert.AreEqual("bar", array[1]);
        }

        [TestMethod]
        public void TestPush()
        {
            var array = RaLisp.Environment.Evaluate("(push (array 'foo') 'bar')") as object[];
            Assert.IsNotNull(array);
            Assert.AreEqual(2, array.Length);
            Assert.AreEqual("foo", array[0]);
            Assert.AreEqual("bar", array[1]);
        }


        [TestMethod]
        public void TestFunctionsCallingFunctions()
        {
            Assert.AreEqual((float) 6, RaLisp.Environment.Evaluate("(+ (+ 1 1) (+ 2 2))"));
        }

        [TestMethod]
        public void TestAddingObjects()
        {
            var code = @"
                (let x (new))
                (let y (new))

                (let x.foo 'FOO')
                (let y.bar 'BAR')

                (+ x y)";
            var output = RaLisp.Environment.Evaluate(code) as IDictionary<string, object>;
            Assert.IsNotNull(output);
            Assert.AreEqual("FOO", output["foo"]);
            Assert.AreEqual("BAR", output["bar"]);

        }

        [TestMethod]
        public void TestMap()
        {
            var code = @"
                (let y (array 1 2 3))
                (let adder (fn x => + x 1))
                (map y adder)
                ";
            var output = RaLisp.Environment.Evaluate(code) as object[];
            Assert.IsNotNull(output);
            Assert.AreEqual((float)2, output[0]);
            Assert.AreEqual((float)3, output[1]);
            Assert.AreEqual((float)4, output[2]);
        }


        [TestMethod]
        public void TestFilter()
        {
            var code = @"
                (let y (array 1 2 3))
                (fn x => = x 2)
                (filter y @)
                ";
            var output = RaLisp.Environment.Evaluate(code) as object[];
            Assert.IsNotNull(output);
            Assert.AreEqual(1, output.Length);
            Assert.AreEqual((float)2, output[0]);
        }

        [TestMethod]
        public void TestNot()
        {
            Assert.IsFalse((bool)RaLisp.Environment.Evaluate("(! true)"));
            Assert.IsTrue((bool)RaLisp.Environment.Evaluate("(! false)"));
        }

        [TestMethod]
        public void TestAddArrays()
        {
            var output = RaLisp.Environment.Evaluate(@"(+ (array 1 2) (array 3 4))") as object[];

            Assert.IsNotNull(output);
            Assert.AreEqual((float) 1, output[0]);
            Assert.AreEqual((float) 2, output[1]);
            Assert.AreEqual((float) 3, output[2]);
            Assert.AreEqual((float) 4, output[3]);
        }

        [TestMethod]
        public void TestFunctionChaining()
        {
            var code = @"
                (array 1 2 3)
                    (map @ (fn x => + x 1))
                    (map @ (fn x => + x 10))
                ";

            var output = RaLisp.Environment.Evaluate(code) as object[];

            Assert.IsNotNull(output);
            Assert.AreEqual(3, output.Length);
            Assert.AreEqual((float)12, output[0]);
            Assert.AreEqual((float)13, output[1]);
            Assert.AreEqual((float)14, output[2]);
        }

        [TestMethod]
        public void TestFunctionChaining2()
        {
            var code = @"
                (fn => 'hello')
                (+ (@) ' world')
                ";

            var output = RaLisp.Environment.Evaluate(code) as string;

            Assert.IsNotNull(output);
            Assert.AreEqual("hello world", output);
        }

        [TestMethod]
        public void TestComment()
        {

            var code = @"
                (comment (fn => 'hello')
                (+ (@) ' world'))
                ";

            var output = RaLisp.Environment.Evaluate(code) as string;
            Assert.IsNull(output);
        }

        [TestMethod]
        public void TestEquals()
        {
            Assert.IsTrue((bool)RaLisp.Environment.Evaluate("(= 'foo' 'foo' 'foo')"));
            Assert.IsFalse((bool)RaLisp.Environment.Evaluate("(= 'foo' 'bar')"));
            Assert.IsTrue((bool)RaLisp.Environment.Evaluate("(= 2 2)"));
            Assert.IsTrue((bool)RaLisp.Environment.Evaluate("(= true true)"));
            Assert.IsTrue((bool)RaLisp.Environment.Evaluate("(= false false)"));
            Assert.IsTrue((bool)RaLisp.Environment.Evaluate("(let x false) (= false x)"));
        }

        [TestMethod]
        public void TestStringEscaping()
        {
            Assert.AreEqual("hel'lo", RaLisp.Environment.Evaluate("(+ 'hel' \"'lo\")"));
        }

        [TestMethod]
        public void TestMultiply()
        {
            Assert.AreEqual((float)10, RaLisp.Environment.Evaluate("(* 2 5)"));
        }

        [TestMethod]
        public void TestSubtract()
        {
            Assert.AreEqual((float)3, RaLisp.Environment.Evaluate("(- 5 2)"));
        }

        [TestMethod]
        public void TestDivide()
        {
            Assert.AreEqual((float)2, RaLisp.Environment.Evaluate("(/ 4 2)"));
        }


        [TestMethod]
        public void TestGreaterThan()
        {
            Assert.IsTrue((bool)RaLisp.Environment.Evaluate("(let x 14) (> 10 12 13.4 x)"));
            Assert.IsFalse((bool)RaLisp.Environment.Evaluate("(> 4 4)"));
        }

        [TestMethod]
        public void TestLessThan()
        {
            Assert.IsFalse((bool)RaLisp.Environment.Evaluate("(let x 14) (< 10 12 13.4 x)"));
            Assert.IsTrue((bool)RaLisp.Environment.Evaluate("(< 5 4)"));
        }

        [TestMethod]
        public void TestRange()
        {
            var result = RaLisp.Environment.Evaluate("(range 1 4)") as double[];
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Length);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(4, result[3]);

            var result2 = RaLisp.Environment.Evaluate("(range 3)") as double[];
            Assert.IsNotNull(result2);
            Assert.AreEqual(3, result2.Length);
            Assert.AreEqual(0, result2[0]);
            Assert.AreEqual(2, result2[2]);

        }

        [TestMethod]
        public void TestTry()
        {

            var result1 = RaLisp.Environment.Evaluate("(try 'ok' 'catch')") as string;
            Assert.AreEqual("ok", result1);

            var result2 = RaLisp.Environment.Evaluate("(try 'ok' 'catch' 'finally')") as string;
            Assert.AreEqual("finally", result2);

            var result3 = RaLisp.Environment.Evaluate("(try (throw 'help') 'catch')") as string;
            Assert.AreEqual("catch", result3);

            var result4 = RaLisp.Environment.Evaluate("(try (throw 'help') @)") as Exception;
            Assert.IsTrue(result4 is Exception);

        }

     
    }


}
