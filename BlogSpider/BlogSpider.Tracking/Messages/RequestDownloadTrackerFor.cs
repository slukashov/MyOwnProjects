using Akka.Actor;
using BlogSpider.Job.States;

namespace BlogSpider.Tracking.Messages
{
    public class RequestDownloadTrackerFor
    {
        public RequestDownloadTrackerFor(CrawlJob key, IActorRef originator)
        {
            Originator = originator;
            Key = key;
        }

        public CrawlJob Key { get; }

        public IActorRef Originator { get; }
    }
}
