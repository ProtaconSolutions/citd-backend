using Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rx;
using Services;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IMessagePublisher>(new MessagePublisher());

        services.AddSingleton<CompilerService, CompilerService>();
        services.AddSingleton<TimerService, TimerService>();
        services.AddSingleton<MessagingOutAdapter, MessagingOutAdapter>();

        services.AddSignalR(o => { 
            o.Hubs.EnableDetailedErrors = true;
        });

        services.BuildServiceProvider()
            .WarmUp<TimerService>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
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
    }
}