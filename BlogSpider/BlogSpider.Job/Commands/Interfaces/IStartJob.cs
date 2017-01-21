using Akka.Actor;
using Akka.Routing;
using BlogSpider.Job.States;

namespace BlogSpider.Job.Commands.Interfaces
{
    public interface IStartJob : IConsistentHashable
    {
        CrawlJob Job { get; }
        IActorRef Requstor { get; }
    }
}
