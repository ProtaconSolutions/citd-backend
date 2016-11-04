using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Citd.Roslyn;

namespace citd_console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var assembly = Compiler.Compile(@"
using System;

public static class Greeter
{
    public static string Greet(int i) 
    {
        return i;
    }
}
");

            var fixture = new TestFixture
            {
                TypeName = "Greeter",
                MethodName = "Greet",
                Tests = new List<Test>
                {
                    Test.OfResultType(typeof(int))
                        .WithInput(1)
                        .WithExpected(1)
                }
            };

            var runner = new TestRunner();
            var result = runner.TestAll(assembly, fixture);

            Console.WriteLine(result.ResultType);
            Console.WriteLine(result.Failure.Message ?? "");

            Console.ReadKey();

        }
    }
}
