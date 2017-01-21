using Akka.Actor;
using Akka.Routing;
using BlogSpider.AkkaConfiguration;
using BlogSpider.Scheduler.Actors;
using Topshelf;

namespace BlogSpider.Scheduler.Scheduler.Service
{
    public class SchedulerService : ServiceControl
    {
        protected ActorSystem ClusterSystem { set; get; }

        public bool Start(HostControl hostControl)
        {
            ClusterSystem = Configuration.LaunchSystem("webcrawler", "Scheduler.hocon");
            var router = ClusterSystem.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "tasker");
            SystemActors.CommandProcessor = ClusterSystem.ActorOf(Props.Create(() => new CommandProcessor(router)), "commands");
            SystemActors.ReCrawlActor = ClusterSystem.ActorOf(Props.Create(() => new ReCrawlActor()), "recrawl");
            Sсheduler.Run().GetAwaiter().GetResult();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            ClusterSystem?.Terminate();
            return true;
        }
    }
}