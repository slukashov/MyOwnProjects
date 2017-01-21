using Akka.Actor;
using BlogSpider.Job.Commands.Interfaces;
using BlogSpider.Job.States;

namespace BlogSpider.Job.Commands
{
    public class UnsubscribeFromJob : ISubscribeUnsucscribeJob
    {
        public UnsubscribeFromJob(CrawlJob job, IActorRef subscriber)
        {
            Job = job;
            Subscriber = subscriber;
        }

        public CrawlJob Job { get; }
        public IActorRef Subscriber { get; }
    }
}
