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
            .Build();

            host.Run(); 
        }
    }
}
