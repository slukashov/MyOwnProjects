using BlogSpider.Job.States;

namespace BlogSpider.Tracking.Messages
{
    public class FindRunningJob
    {
        public FindRunningJob(CrawlJob key)
        {
            Key = key;
        }

        public CrawlJob Key { get; }
    }
}

