using BlogSpider.Job.States;

namespace BlogSpider.Tracking.Messages
{
    public class TrackerDead
    {
        public TrackerDead(CrawlJob key)
        {
            Key = key;
        }

        public CrawlJob Key { get; }
    }
}
