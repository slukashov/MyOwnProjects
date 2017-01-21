using Akka.Actor;
using BlogSpider.Job.Commands.Interfaces;
using BlogSpider.Job.States;

namespace BlogSpider.Job.Commands
{
    public class SubscribeToJob : ISubscribeUnsucscribeJob
    {
        public SubscribeToJob(IActorRef subscriber, CrawlJob job)
        {
            Subscriber = subscriber;
            Job = job;
        }

        public CrawlJob Job { get; }
        public IActorRef Subscriber { get; }
    }
}
