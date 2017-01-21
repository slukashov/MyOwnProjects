using System;
using Akka.Actor;
using Akka.Event;
using Akka.Routing;
using BlogSpider.Job.States;
using BlogSpider.Parser.Download.Worker;
using BlogSpider.Parser.HttpFactory;
using BlogSpider.Parser.Messages;

namespace BlogSpider.Parser.Download.Coordinator
{
    public class DownloadCoordinator : ReceiveActor
    {
        #region Constants
        public const string Downloader = "downloader";
        public const string Parser = "parser";
        #endregion

        #region Fields
        protected readonly IActorRef DownloadsTracker;
        protected readonly IActorRef Commander;
        protected IActorRef DownloaderRouter;
        protected IActorRef ParserRouter;
        protected CrawlJob Job;
        protected readonly long MaxConcurrentDownloads;
        private ICancelable publishStatsTask;
        protected CrawlJobStats Stats;
        private readonly ILoggingAdapter logger = Context.GetLogger();
        #endregion

        public DownloadCoordinator(CrawlJob job, IActorRef commander, IActorRef downloadsTracker, long maxConcurrentDownloads)
        {
            Job = job;
            DownloadsTracker = downloadsTracker;
            MaxConcurrentDownloads = maxConcurrentDownloads;
            Commander = commander;
            Stats = new CrawlJobStats(Job);
            Receiving();
        }

        protected override void PreStart()
        {
            if (Context.Child(Downloader).Equals(ActorRefs.Nobody))
            {
                DownloaderRouter = Context.ActorOf(
                    Props.Create(() => new DownloadWorker(HttpClientFactory.GetClient, Self, (int)MaxConcurrentDownloads)).WithRouter(new RoundRobinPool(10)), Downloader);
            }

            if (Context.Child(Parser).Equals(ActorRefs.Nobody))
            {
                ParserRouter = Context.ActorOf(Props.Create(() => new ParseWorker.ParseWorker(Job, Self)).WithRouter(new RoundRobinPool(10)), Parser);
            }

            publishStatsTask = new Cancelable(Context.System.Scheduler);
            Context.System.Scheduler.ScheduleTellRepeatedly(TimeSpan.FromMilliseconds(250), TimeSpan.FromMilliseconds(250), Self, PublishStatsTick.Instance, Self, publishStatsTask);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            PostStop();
        }

        protected override void PostStop()
        {
            publishStatsTask.Cancel();
        }

        private void Receiving()
        {
            Receive<PublishStatsTick>(stats =>
            {
                if (!Stats.IsEmpty)
                {
                    logger.Info($"Publishing {Stats} to parent");
                    Commander.Tell(Stats.Copy());
                    Stats = Stats.Reset();
                }
            });

            Receive<CheckDocuments>(documents =>
            {
                DownloadsTracker.Tell(documents, Self);
            });

            Receive<DiscoveredDocuments>(discovered =>
            {
                Stats = Stats.WithDiscovered(discovered);
            });

            Receive<ProcessDocuments>(process =>
            {
                foreach (var doc in process.Documents)
                {
                    if (doc.IsImage)
                    {
                        Context.Parent.Tell(new DownloadWorker.DownloadImage(doc));
                    }
                    else
                    {
                        Context.Parent.Tell(new DownloadWorker.DownloadHtmlDocument(doc));
                    }
                }
            });

            Receive<DownloadWorker.IDownloadDocument>(download =>
            {
                DownloaderRouter.Tell(download);
            });

            Receive<CompletedDocument>(completed =>
            {
                Stats = Stats.WithCompleted(completed);
            });

            Receive<DownloadWorker.RequestParseActor>(request =>
            {
                Sender.Tell(new DownloadWorker.SetParseActor(ParserRouter));
            });

            Receive<RequestDownloadActor>(request =>
            {
                Sender.Tell(new SetDownloadActor(DownloaderRouter));
            });
        }
    }
}