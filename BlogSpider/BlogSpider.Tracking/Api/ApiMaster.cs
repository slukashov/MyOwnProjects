using System;
using System.Linq;
using Akka.Actor;
using Akka.Routing;
using BlogSpider.Tracking.Messages;
using BlogSpider.Job.Commands.Interfaces;
using BlogSpider.Job.Commands;
using BlogSpider.Tracking.ToUri;

namespace BlogSpider.Tracking.Api
{
    public class ApiMaster : ReceiveActor, IWithUnboundedStash
    {
        public const string MasterBroadcastName = "broadcaster";
        protected IActorRef ApiBroadcaster;
        protected IStartJob JobToStart;
        protected int OutstandingAcknowledgements;
        public IStash Stash { get; set; }
        
        public ApiMaster()
        {
            Ready();
        }

        protected override void PreStart()
        {
            ApiBroadcaster = Context.Child(MasterBroadcastName).Equals(ActorRefs.Nobody)
                ? Context.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), MasterBroadcastName)
                : Context.Child(MasterBroadcastName);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            PostStop();
        }

        private void Ready()
        {
            Receive<IStartJob>(jobToStart =>
            {
                JobToStart = jobToStart;

                ApiBroadcaster.Tell(new FindRunningJob(jobToStart.Job));

                Context.ActorSelection("/user/downloads").Tell(new RequestDownloadTrackerFor(jobToStart.Job, Self));

                Become(SearchingForJob);
                var listOfRoutees = ApiBroadcaster.Ask<Routees>(new GetRoutees()).Result.Members.ToList();
                OutstandingAcknowledgements = listOfRoutees.Count();
                Context.SetReceiveTimeout(TimeSpan.FromSeconds(3.0));
            });
            Receive<FindRunningJob>(job => HandleFindRunningJob(job));
        }

        private void SearchingForJob()
        {
            Receive<IStartJob>(startJob => Stash.Stash());

            Receive<FindRunningJob>(findRunningJob => HandleFindRunningJob(findRunningJob));

            Receive<JobFound>(jobFound =>
            {
                if (jobFound.Key.Equals(JobToStart.Job))
                {
                    jobFound.CrawlMaster.Tell(new SubscribeToJob(JobToStart.Requstor, JobToStart.Job));
                    BecomeReady();
                }
            });

            Receive<ReceiveTimeout>(receiveTimeout => ApiBroadcaster.Tell(new JobNotFound(JobToStart.Job)));

            Receive<JobNotFound>(jobNotFound =>
            {
                if (jobNotFound.Key.Equals(JobToStart.Job))
                {
                    OutstandingAcknowledgements--;
                    if (OutstandingAcknowledgements <= 0)
                    {
                        var crawlMaster = Context.ActorOf(Props.Create(() => new CrawlMaster.CrawlMaster(JobToStart.Job)),
                            JobToStart.Job.Root.ToActorName());
                        crawlMaster.Tell(JobToStart);
                        ApiBroadcaster.Tell(new JobFound(JobToStart.Job, crawlMaster));
                    }
                }
            });
        }

        private void BecomeReady()
        {
            Become(Ready);
            Context.SetReceiveTimeout(null);
            Stash.UnstashAll();
        }

        private void HandleFindRunningJob(FindRunningJob job)
        {
            var haveChild = Context.Child(job.Key.Root.ToString());

            if (!haveChild.Equals(ActorRefs.Nobody))
            {
                ApiBroadcaster.Tell(new JobFound(job.Key, haveChild));
            }
        }
    }
}
