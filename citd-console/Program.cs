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

public static class Calc
{
    public static int Summer(int x, int y) 
    {
        return x + 1;
    }
}
";
            var fixture = new TestFixture
            {
                TypeName = "Calc",
                MethodName = "Summer",
                Tests = new List<Test>
                {
                    Test.OfResultType(typeof(int))
                        .WithInput(1,1)
                        .WithExpected(2),
                    Test.OfResultType(typeof(int))
                        .WithInput(1,3)
                        .WithExpected(4)
                }
            };


            var result = TestRunner.Run(code, fixture);

            Console.WriteLine(result.ResultType);
            Console.WriteLine(result.Message);

            Console.ReadKey();

        }
    }
}
