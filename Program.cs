using Microsoft.AspNetCore.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        var host = new WebHostBuilder()
        .UseKestrel()
        .UseIISIntegration()
        .UseStartup<Startup>()
        .UseUrls("http://0.0.0.0:5000")
        .Build();

        host.Run(); 
    }
}   
