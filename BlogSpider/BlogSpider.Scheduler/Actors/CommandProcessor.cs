using System;
using System.Linq;
using Akka.Actor;
using Akka.Routing;
using BlogSpider.Job.Commands;
using BlogSpider.Job.States;
using BlogSpider.Scheduler.Actors.Messages;

namespace BlogSpider.Scheduler.Actors
{
    public class CommandProcessor : ReceiveActor
    {
        protected readonly IActorRef CommandRouter;

        public CommandProcessor(IActorRef commandRouter)
        {
            CommandRouter = commandRouter;
            Receives();
        }

        private void Receives()
        {
            Receive<AttemptCrawl>(attempt =>
            {
                if (Uri.IsWellFormedUriString(attempt.RawString, UriKind.Absolute))
                {
                    var startJob = new StartJob(Sender,new CrawlJob(new Uri(attempt.RawString, UriKind.Absolute),true));
                    CommandRouter.Tell(startJob);
                    CommandRouter.Ask<Routees>(new GetRoutees()).ContinueWith(taskRoutee =>
                    {
                        var debugCluster = new DebugCluster($"{CommandRouter} has {taskRoutee.Result.Members.Count()} routees: {string.Join(",", taskRoutee.Result.Members.Select(routee => routee.ToString()))}");

                        return debugCluster;
                    }).PipeTo(Sender);
                    Sender.Tell(startJob);
                }
                else
                {
                    Sender.Tell(new BadCrawlAttempt(attempt.RawString, "Not an absolute URI"));
                }
            });
        }
    }
}
