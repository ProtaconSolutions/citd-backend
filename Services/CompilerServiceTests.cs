using System;
using Compiler;
using FluentAssertions;
using Hubs;
using Rx;
using Xunit;

namespace Services 
{
    public class CompilerServiceTests 
    {
        private MessagePublisher _messager;
        private CompilerService _compiler;

        public CompilerServiceTests() 
        {
            _messager = new MessagePublisher();
            _compiler = new CompilerService(_messager);
        } 

        [Fact()]
        public void WithValidCode_ThenDontCrash()
        {
            var code = @"
            public class Foo {}
            ";

            TotalTestsResult result = new TotalTestsResult();

            _messager.OfType<ResponseMessage>()
                .Subscribe(x => 
                {
                    result = (TotalTestsResult)x.Data;
                });

            _messager.Publish(new RequestMessage("compileRequest", "senderId", new { Code = code }));

            result.Message.Should().Be("a");
        }
    }
}