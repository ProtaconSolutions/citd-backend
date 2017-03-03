using System;
using System.Reflection;

namespace Compiler
{
    public class TestRunner
    {
        public static TotalTestsResult Run(string code, TestFixture fixture)
        {
            if(code == null) 
            {
                throw new ArgumentNullException(nameof(code));    
            }

            var passedTests = 0;

            try
            {
                Assembly assembly;

                try
                {
                    assembly = Compiler.Compile(code);
                }
                catch (Exception ex)
                {
                    return new TotalTestsResult
                    {
                        ResultType = TestResultType.BuildFailure,
                        NumberOfTests = fixture.Tests.Count,
                        PassedTests = passedTests,
                        Message = ex.ToString()
                    };
                }

                foreach (var test in fixture.Tests)
                {
                    var method = assembly?.GetType(fixture.TypeName)?.GetMethod(fixture.MethodName);

                    if(method == null) 
                    {
                        throw new InvalidOperationException("Cannot load test method, found 'null'");
                    }

                    var result = test.Run(method.Invoke);

                    if (result.Type == TestResultType.TestFailure)
                    {
                        return new TotalTestsResult
                        {
                            ResultType = TestResultType.TestFailure,
                            NumberOfTests = fixture.Tests.Count,
                            PassedTests = passedTests,
                            Message = result.Message
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
            catch (Exception ex)
            {
                return new TotalTestsResult
                {
                    ResultType = TestResultType.RandomFailure,
                    NumberOfTests = fixture.Tests.Count,
                    PassedTests = passedTests,
                    Message = ex.ToString()
                };
            }

        }
    }
}