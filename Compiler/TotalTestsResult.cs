namespace Compiler
{
    public class TotalTestsResult
    {
        public TestResultType ResultType { get; set; }
        public int NumberOfTests { get; set; }
        public int PassedTests { get; set; }
        public TestResult Failure { get; set; }
        public string Message { get; set; }
    }
}