using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Akka.Actor;
using BlogSpider.Database.Enities;
using BlogSpider.Database.Repository.WriteRepository;
using BlogSpider.Job.States;
using BlogSpider.Parser.Download.Worker;
using BlogSpider.Parser.Messages;
using HtmlAgilityPack;
using MongoDB.Driver;

namespace BlogSpider.Parser.ParseWorker
{
    public class ParseWorker : ReceiveActor, IWithUnboundedStash
    {
        protected readonly IActorRef CoordinatorActor;
        protected readonly CrawlJob JobRoot;
        protected IActorRef DownloadActor;

        public ParseWorker(CrawlJob jobRoot, IActorRef coordinatorActor)
        {
            JobRoot = jobRoot;
            CoordinatorActor = coordinatorActor;
            WaitingForDownloadActor();
        }

        public IStash Stash { get; set; }

        protected override void PreStart()
        {
            CoordinatorActor.Tell(new RequestDownloadActor());
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Stash.UnstashAll();
            base.PreRestart(reason, message);
        }

        private void WaitingForDownloadActor()
        {
            Receive<SetDownloadActor>(download =>
            {
                DownloadActor = download.Downloader;
                BecomeParsing();
            });

            ReceiveAny(obj => Stash.Stash());
        }

        private void BecomeParsing()
        {
            Become(Parsing);
            Stash.UnstashAll();
        }

        private void Parsing()
        {
            Receive<DownloadWorker.DownloadHtmlResult>(downloadHtmlResult =>
            {
                var mongoClient = new MongoClient();
                var repository = new WriteRepository(mongoClient, "Crawler", "CrawlingStats");
                var requestedUrls = new List<CrawlDocument>();
                var htmlString = downloadHtmlResult.Content;
                var doc = new HtmlDocument();
                doc.LoadHtml(htmlString);

                var allTags = doc.DocumentNode.SelectNodes("//*");
                var allLinks = doc.DocumentNode.SelectNodes("//a[@href]");

                if (allLinks != null)
                {
                    var validUris =
                        allLinks.Select(htmlNode => htmlNode.Attributes["href"].Value)
                            .Where(CanMakeAbsoluteUri)
                            .Select(ToAsboluteUri)
                            .Select(documentUri => new CrawlDocument(documentUri));

                    requestedUrls = requestedUrls.Concat(validUris).ToList();
                }

                if (allTags != null)
                {
                    var allTagsWithTypeAttribute = allTags.Where(tag => tag.Attributes.Contains("type"));

                    var tagsWithRss = allTagsWithTypeAttribute.SelectMany(tag => tag.Attributes
                        .AttributesWithName("type")
                        .Where(attribute => attribute.Value == "application/rss+xml"), (tag, attribute) => tag);

                    foreach (var link in tagsWithRss)
                    {
                        var linkToSite =
                            link.Attributes.AttributesWithName("href")
                                .FirstOrDefault(attribute => attribute.Name == "href");
                        if (linkToSite != null)
                        {
                            Uri uri;
                            if (Uri.TryCreate(linkToSite.Value, UriKind.Absolute, out uri))
                            {
                                var xmlDocument = new XmlDocument();
                                try
                                {
                                    xmlDocument.Load(uri.ToString());

                                    var nodes = xmlDocument.GetElementsByTagName("link");

                                    var validLinksUris = nodes.Cast<XmlNode>()
                                        .Where(node => IsUriCorrect(node.InnerText))
                                        .Select(node => new CrawlDocument(new Uri(node.InnerText)))
                                        .ToList();

                                    requestedUrls = requestedUrls.Concat(validLinksUris).ToList();
                                }
                                catch (Exception)
                                {
                                    // ignoged
                                }
                            }
                        }
                    }
                }
                if (requestedUrls.Count != 0)
                {
                    var listOfBsonDocuments = requestedUrls
                        .Select(link => new Enitity(DateTime.Today, link.DocumentUri.AbsoluteUri)
                            .ToBsonDocument());
                    RunTask(() => repository.WriteRangeToCollectionAsync(listOfBsonDocuments));

                    CoordinatorActor.Tell(
                        new CheckDocuments(requestedUrls, DownloadActor,
                            TimeSpan.FromMilliseconds(requestedUrls.Count * 5000)), Self);
                }
            });

            Receive<SetDownloadActor>(download => { DownloadActor = download.Downloader; });
        }

        private bool IsUriCorrect(string uri)
        {
            return !string.IsNullOrEmpty(uri) ||
                   !string.IsNullOrWhiteSpace(uri);
        }

        #region URI formatting tools

        public bool CanMakeAbsoluteUri(string rawUri)
        {
            if (Uri.IsWellFormedUriString(rawUri, UriKind.Absolute))
                return true;
            try
            {
                var absUri = new Uri(JobRoot.Root, rawUri);
                return absUri.Scheme.Equals(Uri.UriSchemeHttp) || absUri.Scheme.Equals(Uri.UriSchemeHttps);
            }
            catch
            {
                return false;
            }
        }

        public bool AbsoluteUriIsInDomain(Uri otherUri)
        {
            return JobRoot.Domain == otherUri.AbsoluteUri;
        }

        public Uri ToAsboluteUri(string rawUri)
        {
            return Uri.IsWellFormedUriString(rawUri, UriKind.Absolute)
                ? new Uri(rawUri, UriKind.Absolute)
                : new Uri(JobRoot.Root, rawUri);
        }

        #endregion
    }
}