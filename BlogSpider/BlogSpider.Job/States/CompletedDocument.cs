using Akka.Actor;

namespace BlogSpider.Job.States
{
    public class CompletedDocument
    {
        public CompletedDocument(CrawlDocument document, int numBytes, IActorRef completedBy)
        {
            CompletedBy = completedBy;
            NumBytes = numBytes;
            Document = document;
        }

        public CrawlDocument Document { get; }

        public int NumBytes { get; }

        public IActorRef CompletedBy { get; }
    }
}
