using Akka.Actor;
using BlogSpider.Job.States;

namespace BlogSpider.Tracking.Messages
{
    public class JobFound
    {
        public JobFound(CrawlJob key, IActorRef crawlMaster)
        {
            CrawlMaster = crawlMaster;
            Key = key;
        }

        public CrawlJob Key { get; }
        public IActorRef CrawlMaster { get; }
    }
}
