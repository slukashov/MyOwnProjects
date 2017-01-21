using Akka.Actor;
using BlogSpider.Job.Commands.Interfaces;
using BlogSpider.WebApplication.Actors.Messages;
using BlogSpider.WebApplication.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace BlogSpider.WebApplication.Actors
{
    public class SignalRActor : ReceiveActor
    {
        private CrawlHub hub;

        public SignalRActor()
        {
            Receive<string>(str =>
            {
                SystemActors.CommandProcessor.Tell(new AttemptCrawl(str));
            });

            Receive<BadCrawlAttempt>(bad =>
            {
                hub.CrawlFailed($"COULD NOT CRAWL {bad.RawString}: {bad.Message}");
            });

            Receive<IStatusUpdate>(status =>
            {
                hub.PushStatus(status);
            });

            Receive<IStartJob>(start =>
            {
                hub.WriteRawMessage($"Starting crawl of {start.Job.Root.ToString()}");
            });

            Receive<DebugCluster>(debug =>
            {
                hub.WriteRawMessage($"DEBUG: { debug.Message}");
            });
        }

        protected override void PreStart()
        {
            var hubManager = new DefaultHubManager(GlobalHost.DependencyResolver);
            hub = hubManager.ResolveHub("crawlHub") as CrawlHub;
        }
    }
}
