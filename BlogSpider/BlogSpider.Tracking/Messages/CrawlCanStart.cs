using BlogSpider.Job.Commands.Interfaces;

namespace BlogSpider.Tracking.Messages
{
    public class CrawlCanStart
    {
        public IStartJob Job { get;  }
        public int NodeCount { get; }

        public CrawlCanStart(IStartJob job, int nodeCount)
        {
            Job = job;
            NodeCount = nodeCount;
        }
    }
}
