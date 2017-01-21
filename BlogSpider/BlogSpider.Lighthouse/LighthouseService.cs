using Akka.Actor;
using Topshelf;
using BlogSpider.AkkaConfiguration;

namespace BlogSpider.Lighthouse
{
    public class LighthouseService : ServiceControl
    {
        private ActorSystem lighthouseSystem;

        public bool Start(HostControl hostControl)
        {
            lighthouseSystem = Configuration.LaunchSystem("webcrawler", "Lighthouse.hocon");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            lighthouseSystem.Terminate();
            return true;
        }
    }
}
