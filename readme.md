# RaLisp

> A fun project to learn about language design

## Overview

RaLisp is an interpreted script, with a lisp-style syntax. It does not attempt compatibility with any lisp standard. Instead, its a science experiment. It is a single .NET 4.6 binary, which you can:

* Add as a reference to your own project
* Execute on the command line as a REPL (run `RaLisp.exe`)
* Use to execute a RaLisp program (run `RaLisp.exe program.txt`)

RaLisp is in very early stages of development. It is not intended for production use.

## Syntax

Hello World

```lisp
(print "hello world")
```

Basic operators

```lisp
;; addition
(+ 1 2)
(+ 1 2 3 4)
(+ 'hello' " world")

;; subtraction
(- 5 2)

;; multiplication
(* 2 6)

;; division
(/ 1 2)
```
more maths will be added later!

Boolean logic:

```lisp
;; and
(& true true true) 

;; or
(| true false) 

;; not
(! false)

;; if 
(? true (print "true") (print "false"))

;; equals
(= 'foo' 'foo' 'foo')
(= 2 2)
(= true true)

;; great and less than
(> 3 4 5)
(< 5 4 3)
```

Variables

```lisp
;; bind a variable
(let a "foo")
(let b 1)
(let c false)
```

Objects

```lisp
;; create an object
(let y (new))
;; assign a property
(let y.foo "FOO")
(print y.foo)

(let z (new))
(let z.bar "BAR")
;; add two objects together
(+ y z)
```

Arrays

```lisp
;; define an array
(let qux (array 1 2 3))

;; add something to the array
(let qux` (push qux 4))

;; add two arrays together
(+ qux qux`)
```
more array methods will be added later!

Functions

```lisp
;; define a function that adds one
(let addone (fn a => + a 1 ))

;; map an array
(map (array 1 2 3) addone)

;; the result of the previous statement can be accessed using @
;; this allows functions to be chained together
(array 1 2 3)
	(map @ addone)
	(push @ 5)

;; another example
(fn => 'hello')
    (+ (@) ' world')	
```

Modules

```lisp
;; other modules can be loaded and assigned to a variable
(let http (require "http.txt"))
(http.get "http://localhost")


;; modules can export functions or objects by assigning them to an export object
(let export.get (fn url => ...))
```

...of course, I'll need to write some modules first!

Comments

```lisp
(comment this is a comment)
```

.NET interop

```C#
// A function can be added to the context by implementing the IFunction interface, and ensuring the assembly is loaded.

public class Print : IFunction
{
    public string Name
    {
        get
        {
            return "print";
        }
    }

    public object Execute(IDictionary<string, object> context, params object[] parameters)
    {
        Console.WriteLine(string.Join(",", parameters.Select(x => x.Evaluate(context).ToString())));
        return null;
    }
}
```

## License 

MIT



