using System;
using System.Collections.Generic;
using System.Linq;
using BlogSpider.Job.States;
using HtmlAgilityPack;

namespace BlogSpider.Buffer
{
    public class Parser
    {
        private List<CrawlDocument> requestedUrls;
        public CrawlJob CrawlJob { get; }

        public Parser(CrawlJob job)
        {
            CrawlJob = job;
        }

        public  void BufferToParse(string htmlString)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlString);

            var imgs = doc.DocumentNode.SelectNodes("//img[@src]");

            var links = doc.DocumentNode.SelectNodes("//a[@href]");

            if (imgs != null)
            {
                var validImgUris =
                    imgs.Select(x => x.Attributes["src"].Value)
                        .Where(CanMakeAbsoluteUri)
                        .Select(ToAsboluteUri)
                        .Where(AbsoluteUriIsInDomain)
                        .Select(y => new CrawlDocument(y, true));

                requestedUrls = requestedUrls.Concat(validImgUris).ToList();
            }

            if (links != null)
            {
                var validLinkUris =
                    links.Select(x => x.Attributes["href"].Value)
                        .Where(CanMakeAbsoluteUri)
                        .Select(ToAsboluteUri)
                        .Where(AbsoluteUriIsInDomain)
                        .Select(y => new CrawlDocument(y));

                requestedUrls = requestedUrls.Concat(validLinkUris).ToList();
            }
        }
        #region URI formatting tools

        private  bool CanMakeAbsoluteUri(string rawUri)
        {
            if (Uri.IsWellFormedUriString(rawUri, UriKind.Absolute))
                return true;
            try
            {
                var absUri = new Uri(CrawlJob.Root, rawUri);
                var returnVal = absUri.Scheme.Equals(Uri.UriSchemeHttp) || absUri.Scheme.Equals(Uri.UriSchemeHttps);
                return returnVal;
            }
            catch
            {
                return false;
            }
        }

        private  bool AbsoluteUriIsInDomain(Uri otherUri)
        {
            return CrawlJob.Domain == otherUri.Host;
        }

        private  Uri ToAsboluteUri(string rawUri)
        {
            return Uri.IsWellFormedUriString(rawUri, UriKind.Absolute) ? new Uri(rawUri, UriKind.Absolute) : new Uri(CrawlJob.Root, rawUri);
        }

        #endregion
    }
}
