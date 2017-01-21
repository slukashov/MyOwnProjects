using Akka.Actor;
using BlogSpider.Job.Commands.Interfaces;
using BlogSpider.Job.States;

namespace BlogSpider.Job.Commands
{
    public class StartJob : IStartJob
    {
        public StartJob(IActorRef requstor, CrawlJob job)
        {
            Requstor = requstor;
            Job = job;
        }

        public object ConsistentHashKey => Job.Root.OriginalString; 
        public CrawlJob Job { get; }
        public IActorRef Requstor { get; }
    }
}
