using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Hubs;
using Rx;

namespace Services 
{
    public class TimerService 
    {
        private const int _countdown = 60;
    
        public TimerService(IMessagePublisher messages) 
        {
            Observable.Interval(TimeSpan.FromMilliseconds(1000))
                .Subscribe(x =>
                {
                    var timeleft = _countdown - (x % _countdown);
                    
                    messages.Publish(new BroadcastMessage("time", new {
                        Interval = _countdown,
                        TimeLeft = timeleft
                    }));

                    // This should be removed at state where backend has all current code.
                    if(timeleft == 0) {
                        messages.Publish(new BroadcastMessage("compileNeeded", new {}));
                    }
                });
        }
    }
}