using System;

namespace Citd.Roslyn
{
    public class Test
    {
        private static TestObject _test;
        private static Type _type;

        private Test()
        {
        }

        public static Test OfResultType(Type type)
        {
            _type = type;
            _test = new TestObject();
            return new Test();
        }

        public TestResult Run(Func<object, object[], object> invoke)
        {
            var result = Convert.ChangeType(invoke(null, _test.Input), _type);
            var expected = Convert.ChangeType(_test.Expected, _type);

            if (result.ToString() != expected.ToString())
            {
                return new TestResult(
                    TestResultType.Failure,
                    $"Test failed with input {_test.Input}, expected result {expected} but was {result}");
            }

            return new TestResult(TestResultType.Ok, "");
        }

        public Test WithInput(params object[] args)
        {
            _test.Input = args;
            return this;
        }

        public Test WithExpected(object expected)
        {
            _test.Expected = expected;
            return this;
        }

        private class TestObject
        {
            public object[] Input { get; set; }
            public object Expected { get; set; }
        }

    }
}