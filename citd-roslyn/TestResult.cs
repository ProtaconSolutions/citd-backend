namespace Citd.Roslyn
{
    public struct TestResult
    {
        public TestResultType Type { get; private set; }

        public TestResult(TestResultType type, string s)
        {
            Type = type;
        }
    }
}