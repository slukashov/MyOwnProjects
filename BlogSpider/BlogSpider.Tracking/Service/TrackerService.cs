using Akka.Actor;
using BlogSpider.Tracking.Api;
using Topshelf;
using BlogSpider.Tracking.Download;
using BlogSpider.AkkaConfiguration;

namespace BlogSpider.Tracking.Service
{
    public class TrackerService : ServiceControl
    {
        protected ActorSystem ClusterSystem;
        protected IActorRef ApiMaster;
        protected IActorRef DownloadMaster;

        public bool Start(HostControl hostControl)
        {
            ClusterSystem = Configuration.LaunchSystem("webcrawler", "Tracking.hocon");
            ApiMaster = ClusterSystem.ActorOf(Props.Create(() => new ApiMaster()), "api");
            DownloadMaster = ClusterSystem.ActorOf(Props.Create(() => new DownloadMaster()), "downloads");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            ClusterSystem.Terminate();
            return true;
        }
    }
}