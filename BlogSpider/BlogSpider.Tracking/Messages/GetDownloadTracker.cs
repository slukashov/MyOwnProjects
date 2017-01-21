using BlogSpider.Job.States;

namespace BlogSpider.Tracking.Messages
{
    public class GetDownloadTracker
    {
        public GetDownloadTracker(CrawlJob key)
        {
            Key = key;
        }

        public CrawlJob Key { get; }
    }
}
