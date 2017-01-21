using Akka.Actor;
using Topshelf;
using BlogSpider.AkkaConfiguration;

namespace BlogSpider.BlogCrawler
{
    public class CrawlerService : ServiceControl
    {
        protected ActorSystem ClusterSystem { set; get; }

        public bool Start(HostControl hostControl)
        {
            ClusterSystem = Configuration.LaunchSystem("webcrawler", "Crawler.hocon");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            ClusterSystem?.Terminate();
            return true;
        }
    }
}
