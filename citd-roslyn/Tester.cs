using System.Collections.Generic;
using System.Reflection;

namespace Citd.Roslyn
{
    public class Tester
    {
        private static readonly List<Test> Tests = new List<Test>
        {
            Test.OfResultType(typeof(int))
                .WithInput(new[] {200, 300})
                .WithExpected(60000)
        };


        public TotalTestsResult TestAll(Assembly assembly)
        {
            var passedTests = 0;

            foreach (var test in Tests)
            {
                var result = test.Run(assembly.GetType("Area").GetMethod("GetArea").Invoke);

                if (result.Type == TestResultType.Failure)
                {
                    return new TotalTestsResult
                    {
                        ResultType = TestResultType.Failure,
                        NumberOfTests = Tests.Count,
                        PassedTests = passedTests,
                        Failure = result
                    };
                }

                passedTests++;
            }

            return new TotalTestsResult
            {
                ResultType = TestResultType.Ok,
                NumberOfTests = Tests.Count,
                PassedTests = Tests.Count
            };
        }
    }
}