using Akka.Actor;
using System.Collections.Generic;
using System.Linq;

namespace BlogSpider.Job.States
{
    public class DiscoveredDocuments
    {
        public DiscoveredDocuments(IList<CrawlDocument> documents, IActorRef discoveredBy)
        {
            DiscoveredBy = discoveredBy;
            Documents = documents;
        }

        public IList<CrawlDocument> Documents { get; }

        public int HtmlDocs => Documents.Count(x => !x.IsImage);

        public int Images => Documents.Count(x => x.IsImage);

        public IActorRef DiscoveredBy { get; }
    }
}
