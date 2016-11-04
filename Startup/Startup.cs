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
        services.AddSingleton<IMessagePublisher, MessagePublisher>();

        services.AddSingleton<CompilerService, CompilerService>();
        services.AddSingleton<TimerService, TimerService>();
        services.AddSingleton<MessagingOutAdapter, MessagingOutAdapter>();
        
        services.AddSignalR();

        services.BuildServiceProvider()
            .WarmUp<TimerService>()
            .WarmUp<MessagingOutAdapter>();
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
    }
}