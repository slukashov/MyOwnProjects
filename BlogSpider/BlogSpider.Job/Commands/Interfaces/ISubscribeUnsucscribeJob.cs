using Akka.Actor;
using BlogSpider.Job.States;

namespace BlogSpider.Job.Commands.Interfaces
{
   public interface ISubscribeUnsucscribeJob
    {
        CrawlJob Job { get; }
        IActorRef Subscriber { get; }
    }
}
