using Microsoft.AspNetCore.Hosting;

namespace Citd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
            .UseKestrel()
            .UseIISIntegration()
            .UseStartup<Citd.Startup>()
            .UseUrls("http://0.0.0.0:5000")
            .Build();

            host.Run(); 
        }
    }
}
