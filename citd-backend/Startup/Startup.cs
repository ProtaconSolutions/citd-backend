using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Citd
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
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
}