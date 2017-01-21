using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.Event;
using Akka.Routing;
using BlogSpider.Job.Commands;
using BlogSpider.Job.Commands.Interfaces;
using BlogSpider.Job.States;
using BlogSpider.Parser.Download.Coordinator;
using BlogSpider.Parser.Download.Worker;
using BlogSpider.Tracking.Messages;

namespace BlogSpider.Tracking.CrawlMaster
{
    public class CrawlMaster : ReceiveActor, IWithUnboundedStash
    {
        public const string CoordinatorRouterName = "coordinators";
        protected readonly CrawlJob Job;
        protected HashSet<IActorRef> Subscribers = new HashSet<IActorRef>();
        protected JobStatusUpdate RunningStatus;
        protected IActorRef CoordinatorRouter;
        protected IActorRef DownloadTracker;
        protected ICancelable JobStarter;
        protected ILoggingAdapter Log = Context.GetLogger();
        public IStash Stash { get; set; }
        protected CrawlJobStats TotalStats
        {
            get { return RunningStatus.Stats; }
            set { RunningStatus.Stats = value; }
        }

        public CrawlMaster(CrawlJob job)
        {
            Job = job;
            RunningStatus = new JobStatusUpdate(Job);
            TotalStats = new CrawlJobStats(Job);
            Context.SetReceiveTimeout(TimeSpan.FromSeconds(5));
            WaitingForTracker();
        }

        protected override void PreStart()
        {
            Context.ActorSelection("/user/downloads").Tell(new RequestDownloadTrackerFor(Job, Self));
        }

        private void WaitingForTracker()
        {
            Receive<ReceiveTimeout>(timeout => EndJob(JobStatus.Failed));

            Receive<ISubscribeUnsucscribeJob>(subscribe =>
            {
                if (subscribe.Job.Equals(Job))
                    Subscribers.Add(subscribe.Subscriber);
            });

            Receive<ISubscribeUnsucscribeJob>(unsubscribe =>
            {
                if (unsubscribe.Job.Equals(Job))
                    Subscribers.Remove(unsubscribe.Subscriber);
            });

            Receive<TrackerFound>(trackerFound =>
            {
                DownloadTracker = trackerFound.Tracker;
                BecomeReady();
            });

            ReceiveAny(obj => Stash.Stash());
        }

        private void BecomeReady()
        {
            if (Context.Child(CoordinatorRouterName).Equals(ActorRefs.Nobody))
            {
                CoordinatorRouter = 
                    Context.ActorOf(
                        Props.Create(() => new DownloadCoordinator(Job, Self, DownloadTracker, 50))
                            .WithRouter(FromConfig.Instance), CoordinatorRouterName);
            }
            else
            {
                CoordinatorRouter = Context.Child(CoordinatorRouterName);
            }
            Become(Ready);
            Stash.UnstashAll();
            Context.SetReceiveTimeout(TimeSpan.FromSeconds(120));
        }

        private void Ready()
        {
            Receive<IStartJob>(start =>
            {
                Subscribers.Add(start.Requstor);

                JobStarter = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(TimeSpan.FromMilliseconds(20),
                    TimeSpan.FromMilliseconds(20), Self, new AttemptToStartJob(start), Self);
            });

            Receive<AttemptToStartJob>(start =>
            {
                CoordinatorRouter.Ask<Routees>(
                    new GetRoutees()).ContinueWith(taskRoute => new CrawlCanStart(start.Job, taskRoute.Result.Members.Count())).PipeTo(Self);
            });

            Receive<CrawlCanStart>(start => start.NodeCount > 0, start =>
            {
                var downloadRootDocument = new DownloadWorker.DownloadHtmlDocument(new CrawlDocument(start.Job.Job.Root));
                CoordinatorRouter.Tell(downloadRootDocument);
                JobStarter.Cancel();
                Become(Started);
                Stash.UnstashAll();
            });

            Receive<CrawlCanStart>(start =>
            {
                Log.Info("Can't start job yet. No routees.");
            });
            ReceiveAny(obj => Stash.Stash());
        }

        private void Started()
        {
            Receive<IStartJob>(start =>
            {
                if (start.Job.Equals(Job))
                    Subscribers.Add(start.Requstor);
            });

            Receive<ISubscribeUnsucscribeJob>(subscribe =>
            {
                if (subscribe.Job.Equals(Job))
                    Subscribers.Add(subscribe.Subscriber);
            });

            Receive<ISubscribeUnsucscribeJob>(unsubscribe =>
            {
                if (unsubscribe.Job.Equals(Job))
                    Subscribers.Remove(unsubscribe.Subscriber);
            });

            Receive<CrawlJobStats>(stats =>
            {
                TotalStats = TotalStats.Merge(stats);
                PublishJobStatus();
            });

            Receive<StopJob>(stop => EndJob(JobStatus.Stopped));

            Receive<ReceiveTimeout>(timeout => EndJob(JobStatus.Finished));
        }

        private void EndJob(JobStatus finalStatus)
        {
            RunningStatus.Status = finalStatus;
            RunningStatus.EndTime = DateTime.UtcNow;
            PublishJobStatus();
            Self.Tell(PoisonPill.Instance);
        }

        private void PublishJobStatus()
        {
            foreach (var sub in Subscribers)
                sub.Tell(RunningStatus);
        }
    }
}
