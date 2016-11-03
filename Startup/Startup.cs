using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

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
            
            // app.Use(async (context, next) =>
            // {
            //     context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            //     await next.Invoke();
            // });

            app.UseWebSockets();
            app.UseSignalR();
        }
    }
}