using System;
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
        public void IsThereChangeToSkipAnything() 
        {
            _messager.OfType<ResponseMessage>()
                .Subscribe(x => 
                {
                    
                });
                    
            _messager.Publish(new RequestMessage("compileRequest", "senderId", "invalid_code_here"));
        }
    }
}