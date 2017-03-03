using System.Collections.Generic;
using System.Reactive.Linq;
using Compiler;
using Hubs;
using Rx;
using System;

namespace Services 
{
    public class CompilerService 
    {
        public CompilerService(IMessagePublisher messages) 
        {
            messages.OfType<RequestMessage>()
                .Where(x => x.Type == "compileRequest")
                .Subscribe(x => {
                    if(x?.Data?.Code == null)
                        throw new InvalidOperationException("Code cannot be null.");

                    var result = TestRunner.Run(x.Data.Code, TestFixture);

                    messages.Publish(new ResponseMessage("compileResult", x.ConnectionId, result));
                });
        }

        private static readonly TestFixture TestFixture  = new TestFixture
        {
            TypeName = "Math",
            MethodName = "Min",
            Tests = new List<Test>
            {
                Test.OfResultType(typeof(int))
                    .WithInput(0,1,2)
                    .WithExpected(0),
                Test.OfResultType(typeof(int))
                    .WithInput(3,2,1)
                    .WithExpected(1),
                Test.OfResultType(typeof(int))
                    .WithInput(0,-1,1)
                    .WithExpected(-1),
                Test.OfResultType(typeof(int))
                    .WithInput(-1,-1,-1)
                    .WithExpected(-1),
                Test.OfResultType(typeof(int))
                    .WithInput(0,0,1)
                    .WithExpected(0)
            }
        };

    }
}