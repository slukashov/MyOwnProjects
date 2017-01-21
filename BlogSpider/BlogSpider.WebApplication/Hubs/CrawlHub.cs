using Akka.Actor;
using BlogSpider.Job.Commands.Interfaces;
using BlogSpider.WebApplication.Actors;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;

namespace BlogSpider.WebApplication.Hubs
{
    [HubName("crawlHub")]
    public class CrawlHub : Hub
    {
        public void PushStatus(IStatusUpdate update)
        {
            WriteMessage($"[{DateTime.UtcNow}]({update.Job}) - {update.Stats} ({update.Status}) [{update.Elapsed} elapsed]");
        }

        public void CrawlFailed(string reason)
        {
            WriteMessage(reason);
        }

        public void StartCrawl(string message)
        {
            SystemActors.SignalRActor.Tell(message, ActorRefs.Nobody);
        }

        internal void WriteRawMessage(string message)
        {
            WriteMessage(message);
        }

        internal static void WriteMessage(string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<CrawlHub>();
            dynamic allClients = context.Clients.All.writeStatus(message);
        }
    }
}
