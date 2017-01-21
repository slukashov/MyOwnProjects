using Akka.Actor;
using BlogSpider.Job.States;


namespace BlogSpider.Tracking.Messages
{
    public class CreatedTracker
    {
        public CreatedTracker(CrawlJob key, IActorRef tracker)
        {
            Tracker = tracker;
            Key = key;
        }

        public CrawlJob Key { get; }

        public IActorRef Tracker { get; }
    }
}
