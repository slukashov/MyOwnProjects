using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using BlogSpider.Job.States;

namespace BlogSpider.Parser.Messages
{
    public class CheckDocuments
    {
        public CheckDocuments(IList<CrawlDocument> documents, IActorRef requestor, TimeSpan? estimatedCrawlTime)
        {
            EstimatedCrawlTime = estimatedCrawlTime;
            Requestor = requestor;
            Documents = documents;
        }

        public IList<CrawlDocument> Documents { get; }
        public int HtmlDocs => Documents.Count(x => !x.IsImage);
        public int Images => Documents.Count(x => x.IsImage);
        public IActorRef Requestor { get; }
        public TimeSpan? EstimatedCrawlTime { get; }
    }
}
