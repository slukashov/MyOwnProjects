using Akka.Actor;
using BlogSpider.Job.States;

namespace BlogSpider.Tracking.Messages
{
    public class TrackerFound
    {
        public TrackerFound(CrawlJob key, IActorRef tracker)
        {
            Key = key;
            Tracker = tracker;
        }

        public CrawlJob Key { get;  }

        public IActorRef Tracker { get;  }
    }
}
