using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaLisp
{


    public class Statement : IExpression
    {
        public Statement()
        {
            this.Expressions = new List<object>();
        }

        public Statement Parent { get; set; }

        public List<object> Expressions { get; set; }

        public object Evaluate(IDictionary<string, object> context)
        {
            if (this.Expressions.Count == 0) return null;

            if (this.Expressions[0] is Variable)
            {
                var functionName = (this.Expressions[0] as Variable).Name;
                var func = context.Get(functionName) as IFunction;
                var rslt = func.Execute(context, this.Expressions.Skip(1).ToArray());
                context.Set("@", rslt);
                return rslt;
                /*Func<object> action = () => func.Execute(context, this.Expressions.Skip(1).ToArray());
                var task = new Task<object>(action);

                var handle = new ManualResetEvent(false);
                task.ContinueWith(x =>
                {
                    Console.WriteLine("setting wait handle");
                    handle.Set();
                    });

                EventLoop.Instance.Enqueue(task);
                Console.WriteLine("Awaiting wait handle");
                handle.WaitOne();
                Console.WriteLine("resuming wait handle");

                context.Set("@", task.Result);
                return task.Result;*/
            }

            // this is a number of lines of code
            object result = null;
            foreach (Statement statement in this.Expressions)
            {
                result = statement.Evaluate(context);
            }

            context.Set("@", result);

            return result;

        }

    }


}
