using System;
using Akka.Actor;
using Helios.Util.TimedOps;

namespace BlogSpider.Tracking.Status
{
    public class CrawlStatus
    {
        public bool IsComplete { get; private set; }

        public Deadline Timeout { get; private set; }

        public bool CanProcess => !IsComplete && (Timeout == null || Timeout.IsOverdue);

        public IActorRef Owner { get; private set; }

        public static CrawlStatus StartCrawl(IActorRef owner, TimeSpan crawlTime)
        {
            var crawl = new CrawlStatus();
            crawl.TryClaim(owner, crawlTime);
            return crawl;
        }

        public CrawlStatus MarkAsComplete()
        {
            IsComplete = true;
            Timeout = null;
            Owner = null; 
            return this;
        }

        public bool TryClaim(IActorRef newOwner, TimeSpan crawlTime)
        {
            if (CanProcess)
            {
                Timeout = Deadline.Now + crawlTime;
                Owner = newOwner;
                return true;
            }
            return false;
        }
    }
}
