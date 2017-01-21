using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using BlogSpider.Job.States;

namespace BlogSpider.Parser.Messages
{
    public class ProcessDocuments
    {
        public IList<CrawlDocument> Documents { get; }
        public int HtmlDocs => Documents.Count(x => !x.IsImage);
        public int Images => Documents.Count(x => x.IsImage);
        public IActorRef Assigned { get; }

        public ProcessDocuments(IList<CrawlDocument> documents, IActorRef assigned)
        {
            Assigned = assigned;
            Documents = documents;
        }
    }
}
