using System.Collections.Generic;

namespace Compiler
{
    public class TestFixture
    {
        public string TypeName { get; set; }
        public string MethodName { get; set; }
        public List<Test> Tests { get; set; }
    }
}