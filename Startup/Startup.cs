using System;
using System.Reactive.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSignalR();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHubContext<MessagingHub> hub)
    {
        loggerFactory.AddConsole(LogLevel.Debug);

        app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");

            await next.Invoke();
        });

        app.UseWebSockets();
        app.UseSignalR();

        const int countdown = 60;

        Observable.Interval(TimeSpan.FromMilliseconds(1000))
            .Subscribe(x =>
            {
                var timeleft = countdown - (x % countdown);
                
                hub.Clients.Group("time").OnEvent("time", new ChannelEvent()
                {
                    Name = "time",
                    ChannelName = "time",
                    Data = new
                    {
                        Interval = countdown,
                        TimeLeft = timeleft
                    }
                });

                if(timeleft == 0) {
                    hub.Clients.Group("compileRequest").OnEvent("compileRequest", new ChannelEvent {
                        Name = "compileRequest",
                        ChannelName = "compileRequest"
                    });
                }
            });
    }
}