using System.IO;
using Microsoft.AspNetCore.Hosting;
using BlogSpider.WebApplication.Extensions;

namespace BlogSpider.WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseActorSystem()
                .Build();

            host.Run();
        }
    }
}
