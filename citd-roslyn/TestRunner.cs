using System.Reflection;

namespace Citd.Roslyn
{
    public class TestRunner
    {
        public TotalTestsResult TestAll(Assembly assembly, TestFixture fixture)
        {
            var passedTests = 0;

            foreach (var test in fixture.Tests)
            {
                var result = test.Run(assembly.GetType(fixture.TypeName).GetMethod(fixture.MethodName).Invoke);

                if (result.Type == TestResultType.Failure)
                {
                    return new TotalTestsResult
                    {
                        ResultType = TestResultType.Failure,
                        NumberOfTests = fixture.Tests.Count,
                        PassedTests = passedTests,
                        Failure = result
                    };
                }

                passedTests++;
            }

            return new TotalTestsResult
            {
                ResultType = TestResultType.Ok,
                NumberOfTests = fixture.Tests.Count,
                PassedTests = fixture.Tests.Count
            };
        }
    }
}