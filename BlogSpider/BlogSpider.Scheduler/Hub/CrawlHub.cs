using System;
using Akka.Actor;
using BlogSpider.Job.Commands.Interfaces;
using BlogSpider.Scheduler.Actors;

namespace BlogSpider.Scheduler.Hub
{
    public class CrawlHub 
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
            SystemActors.ReCrawlActor.Tell(message, ActorRefs.Nobody);
        }

        internal void WriteRawMessage(string message)
        {
            WriteMessage(message);
        }

        internal static void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
