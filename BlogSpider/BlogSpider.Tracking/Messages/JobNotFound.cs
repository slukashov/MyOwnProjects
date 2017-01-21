using BlogSpider.Job.States;

namespace BlogSpider.Tracking.Messages
{
    public class JobNotFound
    {
        public JobNotFound(CrawlJob key)
        {
            Key = key;
        }

        public CrawlJob Key { get; }
    }
}
