using BlogSpider.Job.States;

namespace BlogSpider.Tracking.Messages
{
    public class TrackerNotFound
    {
        public TrackerNotFound(CrawlJob key)
        {
            Key = key;
        }

        public CrawlJob Key { get; }
    }
}
