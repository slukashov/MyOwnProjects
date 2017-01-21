using Akka.Actor;
using BlogSpider.Job.States;

namespace BlogSpider.Job.Commands
{
    public class StopJob 
    {
        public StopJob(CrawlJob job, IActorRef requestor)
        {
            Requestor = requestor;
            Job = job;
        }

        public CrawlJob Job { get; }
        public IActorRef Requestor { get; }
    }
}
