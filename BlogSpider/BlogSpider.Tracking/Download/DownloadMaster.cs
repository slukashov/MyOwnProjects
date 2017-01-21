using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.Routing;
using BlogSpider.Job.States;
using BlogSpider.Tracking.Messages;
using BlogSpider.Tracking.ToUri;

namespace BlogSpider.Tracking.Download
{
    public class DownloadMaster : ReceiveActor, IWithUnboundedStash
    {
        public const string DownloadsBroadcastName = "broadcaster";
        protected Dictionary<CrawlJob, IActorRef> Trackers = new Dictionary<CrawlJob, IActorRef>();
        protected IActorRef MasterBroadcast;
        protected RequestDownloadTrackerFor RequestedTracker;
        public IStash Stash { get; set; }
        protected int OutstandingAcknowledgments;

        public DownloadMaster()
        {
            Ready();
        }

        protected override void PreStart()
        {
            MasterBroadcast = Context.Child(DownloadsBroadcastName).Equals(ActorRefs.Nobody) ? Context.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), DownloadsBroadcastName)
                : Context.Child(DownloadsBroadcastName);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            PostStop();
        }

        private void Ready()
        {
            Receive<RequestDownloadTrackerFor>(requestDownloadTrackerFor =>
            {
                RequestedTracker = requestDownloadTrackerFor;
                BecomeWaitingForTracker(requestDownloadTrackerFor);
            });

            Receive<GetDownloadTracker>(getDownloadTracker =>
            {
                HandleGetDownloadTracker(getDownloadTracker);
            });

            Receive<TrackerFound>(trackerFound =>
            {
                Trackers[trackerFound.Key] = trackerFound.Tracker;
            });

            Receive<CreatedTracker>(createdTracker =>
            {
                Trackers[createdTracker.Key] = createdTracker.Tracker;
            });

            Receive<TrackerDead>(trackerDead =>
            {
                if (Trackers.ContainsKey(trackerDead.Key))
                {
                    Trackers.Remove(trackerDead.Key);
                }
            });
        }

        private void BecomeWaitingForTracker(RequestDownloadTrackerFor request)
        {
            Become(WaitingForTracker);
            OutstandingAcknowledgments = MasterBroadcast.Ask<Routees>(new GetRoutees()).Result.Members.Count();
            MasterBroadcast.Tell(new GetDownloadTracker(request.Key));
            Context.SetReceiveTimeout(TimeSpan.FromSeconds(1.5));
        }

        private void WaitingForTracker()
        {
            Receive<RequestDownloadTrackerFor>(requestDownloadTrackerFor => Stash.Stash());

            Receive<GetDownloadTracker>(getDownloadTracker =>
            {
                HandleGetDownloadTracker(getDownloadTracker);
            });

            Receive<ReceiveTimeout>(receiveTimeout => Self.Tell(new TrackerNotFound(RequestedTracker.Key)));

            Receive<TrackerNotFound>(trackerNotFound =>
            {
                if (trackerNotFound.Key.Equals(RequestedTracker.Key))
                {
                    OutstandingAcknowledgments--;

                    if (OutstandingAcknowledgments == 0)
                    {
                        var tracker = Context.ActorOf(Props.Create(() => new DownloadTracker()),
                           RequestedTracker.Key.Root.ToActorName());

                        var found = new TrackerFound(RequestedTracker.Key, tracker);
                        BecomeReadyIfFound(found);

                        MasterBroadcast.Tell(new CreatedTracker(RequestedTracker.Key, tracker));
                    }
                }

            });

            Receive<TrackerFound>(trackerFound =>
            {
                Trackers[trackerFound.Key] = trackerFound.Tracker;
                BecomeReadyIfFound(trackerFound);
            });

            Receive<CreatedTracker>(createdTracker =>
            {
                Trackers[createdTracker.Key] = createdTracker.Tracker;

                var trackerFound = new TrackerFound(createdTracker.Key, createdTracker.Tracker);
                BecomeReadyIfFound(trackerFound);
            });

            Receive<TrackerDead>(trackerDead =>
            {
                if (Trackers.ContainsKey(trackerDead.Key))
                {
                    Trackers.Remove(trackerDead.Key);
                }
            });
        }

        private void BecomeReadyIfFound(TrackerFound trackerFound)
        {
            if (trackerFound.Key.Equals(RequestedTracker.Key))
            {
                RequestedTracker.Originator.Tell(trackerFound);
                Become(Ready);
                Stash.UnstashAll();
                Context.SetReceiveTimeout(null);
            }
        }

        private void HandleGetDownloadTracker(GetDownloadTracker getDownloadTracker)
        {
            if (!Context.Child(getDownloadTracker.Key.Root.ToActorName()).Equals(ActorRefs.Nobody))
            {
                var tracker = Context.Child(getDownloadTracker.Key.Root.ToActorName());
                MasterBroadcast.Tell(new TrackerFound(getDownloadTracker.Key, tracker));
            }
            else if (Trackers.ContainsKey(getDownloadTracker.Key))
            {
                var tracker = Trackers[getDownloadTracker.Key];

                tracker.Ask<ActorIdentity>(new Identify(getDownloadTracker.Key), TimeSpan.FromSeconds(1.5))
                    .ContinueWith<object>(taskActorIdentity =>
                    {
                        if (taskActorIdentity.IsCanceled || taskActorIdentity.IsFaulted)
                            return new TrackerDead(getDownloadTracker.Key);
                        return new TrackerFound(getDownloadTracker.Key, taskActorIdentity.Result.Subject);
                    }).PipeTo(MasterBroadcast);
            }
            else
            {
                MasterBroadcast.Tell(new TrackerNotFound(getDownloadTracker.Key));
            }
        }
    }
}
