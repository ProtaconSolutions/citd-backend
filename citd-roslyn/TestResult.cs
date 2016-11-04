namespace Citd.Roslyn
{
    public struct TestResult
    {
        public TestResultType Type { get; private set; }
        public string Message { get; private set; }

        public TestResult(TestResultType type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}