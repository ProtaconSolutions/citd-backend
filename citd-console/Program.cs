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
            const string code = @"
using System;

public static class Greeter
{
    public static int Greet(int i) 
    {
        return 2;
    }
}
";

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
            var result = runner.TestAll(code, fixture);

            Console.WriteLine(result.ResultType);
            Console.WriteLine(result.Message);

            Console.ReadKey();

        }
    }
}
