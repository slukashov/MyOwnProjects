using Topshelf;

namespace BlogSpider.BlogCrawler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(configurator =>
            {
                configurator.Service<CrawlerService>(service => 
                {
                    service.ConstructUsing(name => new CrawlerService());
                    service.WhenStarted((start, hostControl) => start.Start(hostControl));
                    service.WhenStopped((stop, hostControl) => stop.Stop(hostControl));
                });
                configurator.RunAsLocalSystem();
                configurator.SetServiceName("BlogSpiderCrawler");
                configurator.SetDisplayName("BlogSpiderCrawler");
                configurator.SetDescription("Akka.NET Cluster - Blog Spider.");
                configurator.EnableServiceRecovery(serviceRecoveryConfigurator => serviceRecoveryConfigurator.RestartService(1));
            });
        }
    }
}
