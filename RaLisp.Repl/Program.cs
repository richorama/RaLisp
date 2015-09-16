using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaLisp.Repl
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                var environment = new RaLisp.Environment();
                while (true)
                {
                    Console.Write("> ");
                    var line = Console.ReadLine();
                    var output = environment.Eval(line);
                    if (output is object[])
                    {
                        var outputAsArray = output as object[];
                        Console.WriteLine("[ " + string.Join(", ", outputAsArray.Select(x => x.ToString())) + " ]");
                        continue;
                    }

                    if (output is IDictionary<string, object>)
                    {
                        var outputAsObject = output as IDictionary<string, object>;
                        Console.WriteLine("{");
                        foreach (var item in outputAsObject)
                        {
                            Console.WriteLine($"  {item.Key} : {item.Value.ToString()}");
                        }
                        Console.WriteLine("}");
                        continue;
                    }

                    if (output != null) Console.WriteLine(output.ToString());
                }
            }
            else
            {
                var code = File.ReadAllText(args[0]);
                var environment = new RaLisp.Environment(args);
                var returnValue = environment.Eval(code);
                if (returnValue is float)
                {
                    return (int)returnValue;
                }
                else
                {
                    return 0;
                }

            }
        }
    }
}
