using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using Akka.Actor;
using BlogSpider.Job.States;

namespace BlogSpider.Parser.Download.Worker
{
    public class DownloadWorker : ReceiveActor, IWithUnboundedStash
    {
        #region Messages

        public interface IDownloadDocument
        {
            CrawlDocument Document { get; }
        }

        public class DownloadHtmlDocument : IDownloadDocument, IEquatable<DownloadHtmlDocument>
        {
            public DownloadHtmlDocument(CrawlDocument document)
            {
                Document = document;
            }

            public CrawlDocument Document { get; }

            public bool Equals(DownloadHtmlDocument other)
            {
                if (ReferenceEquals(null, other)) return false;
                return ReferenceEquals(this, other) || Equals(Document, other.Document);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == GetType() && Equals((DownloadHtmlDocument)obj);
            }

            public override int GetHashCode() => Document?.GetHashCode() ?? 0;
        }

        public class DownloadImage : IDownloadDocument, IEquatable<DownloadImage>
        {
            public DownloadImage(CrawlDocument document)
            {
                Document = document;
            }

            public CrawlDocument Document { get; }

            public bool Equals(DownloadImage other)
            {
                if (ReferenceEquals(null, other)) return false;
                return ReferenceEquals(this, other) || Equals(Document, other.Document);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == GetType() && Equals((DownloadImage)obj);
            }

            public override int GetHashCode()
            {
                return Document?.GetHashCode() ?? 0;
            }
        }

        public class DownloadImageResult
        {
            public DownloadImageResult(DownloadImage command, byte[] bytes, HttpStatusCode status)
            {
                Status = status;
                Bytes = bytes;
                Command = command;
            }

            public DownloadImage Command { get; }

            public byte[] Bytes { get; }

            public HttpStatusCode Status { get; }
        }

        public class DownloadHtmlResult
        {
            public DownloadHtmlResult(DownloadHtmlDocument command, string content, HttpStatusCode status)
            {
                Status = status;
                Content = content;
                Command = command;
            }

            public DownloadHtmlDocument Command { get; }

            public string Content { get; }

            public HttpStatusCode Status { get; private set; }
        }

        public class SetParseActor
        {
            public SetParseActor(IActorRef parser)
            {
                Parser = parser;
            }

            public IActorRef Parser { get; }
        }

        public class RequestParseActor { }

        #endregion

        private HttpClient httpClient;
        private readonly Func<HttpClient> httpClientFactory;
        public const int DefaultMaxConcurrentDownloads = 50;
        protected int MaxConcurrentDownloads;
        protected int CurrentDownloadCount => currentDownloads.Count;
        protected bool CanDoDownload => CurrentDownloadCount < MaxConcurrentDownloads;
        protected readonly IActorRef CoordinatorActor;
        protected IActorRef ParseActor;
        public IStash Stash { get; set; }
        private readonly HashSet<IDownloadDocument> currentDownloads = new HashSet<IDownloadDocument>();

        public DownloadWorker(Func<HttpClient> httpClientFactory, IActorRef coordinatorActor, int maxConcurrentDownloads = DefaultMaxConcurrentDownloads)
        {
            this.httpClientFactory = httpClientFactory;
            MaxConcurrentDownloads = maxConcurrentDownloads;
            Debug.Assert(maxConcurrentDownloads > 0,"maxConcurrentDownloads must be greater than 0");
            CoordinatorActor = coordinatorActor;
            WaitingForParseActor();
        }

        protected override void PreStart()
        {
            httpClient = httpClientFactory();
            CoordinatorActor.Tell(new RequestParseActor());
        }

        protected override void PostStop()
        {
            try
            {
                httpClient?.Dispose();
            }
            catch (Exception)
            {
                // ignored
            }
            base.PostStop();
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Stash.UnstashAll();
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Become(WaitingForParseActor);
        }

        private void WaitingForParseActor()
        {
            Receive<SetParseActor>(parse =>
            {
                ParseActor = parse.Parser;
                Become(Ready);
                Stash.UnstashAll();
            });

            ReceiveAny(obj => Stash.Stash());
        }

        private void Ready()
        {
            Receive<DownloadHtmlDocument>(downloadHtmlDocument => CanDoDownload, html =>
            {
                if (!(Uri.UriSchemeHttp.Equals(html.Document.DocumentUri.Scheme) ||
                    Uri.UriSchemeHttps.Equals(html.Document.DocumentUri.Scheme)))
                    return;

                currentDownloads.Add(html);
                httpClient.GetStringAsync(html.Document.DocumentUri).ContinueWith(downloadHtmlResult =>
                {
                    // bad request, server error, or timeout
                    if (downloadHtmlResult.IsFaulted || downloadHtmlResult.IsCanceled)
                        return new DownloadHtmlResult(html, string.Empty, HttpStatusCode.BadRequest);

                    // 404
                    return string.IsNullOrEmpty(downloadHtmlResult.Result) ? new DownloadHtmlResult(html, string.Empty, HttpStatusCode.NotFound) : new DownloadHtmlResult(html, downloadHtmlResult.Result, HttpStatusCode.OK);
                }).PipeTo(Self);
            });

            Receive<DownloadImage>(downloadImage => CanDoDownload, image =>
            {
                currentDownloads.Add(image);
                httpClient.GetByteArrayAsync(image.Document.DocumentUri).ContinueWith(downloadHtmlResult =>
                {
                    // bad request, server error, or timeout
                    if (downloadHtmlResult.IsFaulted || downloadHtmlResult.IsCanceled)
                        return new DownloadImageResult(image, new byte[0], HttpStatusCode.BadRequest);

                    // 404
                    if (downloadHtmlResult.Result == null || downloadHtmlResult.Result.Length == 0)
                        return new DownloadImageResult(image, new byte[0], HttpStatusCode.NotFound);

                    return new DownloadImageResult(image, downloadHtmlResult.Result, HttpStatusCode.OK);
                }).PipeTo(Self);
            });

            Receive<IDownloadDocument>(downloadDocument => !CanDoDownload, document =>
            {
                Stash.Stash();
                Become(WaitingForDownloads);
            });

            Receive<DownloadImageResult>(image => HandleImageDownload(image));

            Receive<DownloadHtmlResult>(html => HandleHtmlDownload(html));

            Receive<SetParseActor>(parse => ParseActor = parse.Parser);
        }

        private void WaitingForDownloads()
        {
            Receive<DownloadImageResult>(image =>
            {
                HandleImageDownload(image);
                BecomeReady();
            });

            Receive<DownloadHtmlResult>(html =>
            {
                HandleHtmlDownload(html);
                BecomeReady();
            });

            Receive<IDownloadDocument>(downloadDocument => !CanDoDownload, document => Stash.Stash());

            Receive<SetParseActor>(parse =>
            {
                ParseActor = parse.Parser;
            });
        }

        private void BecomeReady()
        {
            Stash.Unstash();
            Become(Ready);
        }

        #region Download handlers
        private void HandleHtmlDownload(DownloadHtmlResult html)
        {
            currentDownloads.Remove(html.Command);
            CoordinatorActor.Tell(new CompletedDocument(html.Command.Document,html.Content.Length*2, Self));
            ParseActor.Tell(html);
        }

        private void HandleImageDownload(DownloadImageResult image)
        {
            currentDownloads.Remove(image.Command);
            CoordinatorActor.Tell(new CompletedDocument(image.Command.Document, image.Bytes.Length, Self));
        }
        #endregion
    }
}
