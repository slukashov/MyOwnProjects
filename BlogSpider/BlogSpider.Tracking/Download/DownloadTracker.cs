using System;
using System.Collections.Generic;
using Akka.Actor;
using BlogSpider.Job.States;
using BlogSpider.Parser.Messages;
using BlogSpider.Tracking.Status;

namespace BlogSpider.Tracking.Download
{
    public class DownloadTracker: ReceiveActor
    {
        private readonly Dictionary<CrawlDocument, CrawlStatus> recordedDocuments;
        private readonly TimeSpan defaultCrawlTime;

        public DownloadTracker() : this(new Dictionary<CrawlDocument, CrawlStatus>(), TimeSpan.FromSeconds(30)) { }

        public DownloadTracker(Dictionary<CrawlDocument, CrawlStatus> recordedDocuments, TimeSpan defaultCrawlTime)
        {
            this.recordedDocuments = recordedDocuments;
            this.defaultCrawlTime = defaultCrawlTime;
            InitialReceives();
        }

        private void InitialReceives()
        {
            Receive<CheckDocuments>(checkDocuments =>
            {
                var availableDocuments = new List<CrawlDocument>();
                var discoveredDocuments = new List<CrawlDocument>();
                foreach (var crawlDocument in checkDocuments.Documents)
                {
                    if (!recordedDocuments.ContainsKey(crawlDocument))
                    {
                        recordedDocuments[crawlDocument] = CrawlStatus.StartCrawl(checkDocuments.Requestor, checkDocuments.EstimatedCrawlTime ?? defaultCrawlTime);
                        availableDocuments.Add(crawlDocument);
                        discoveredDocuments.Add(crawlDocument);
                    }
                    else if (recordedDocuments[crawlDocument].TryClaim(checkDocuments.Requestor, checkDocuments.EstimatedCrawlTime ?? defaultCrawlTime))
                    {
                        availableDocuments.Add(crawlDocument);
                    }
                }

                Sender.Tell(new ProcessDocuments(availableDocuments, checkDocuments.Requestor));
                Sender.Tell(new DiscoveredDocuments(discoveredDocuments, checkDocuments.Requestor));
            });

            Receive<CompletedDocument>(doc =>
            {
                if (!recordedDocuments.ContainsKey(doc.Document))
                    recordedDocuments[doc.Document] = CrawlStatus.StartCrawl(doc.CompletedBy, defaultCrawlTime);
                recordedDocuments[doc.Document].MarkAsComplete();
            });
        }
    }
}
