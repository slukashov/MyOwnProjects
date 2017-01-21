using BlogSpider.Job.Commands.Interfaces;

namespace BlogSpider.Tracking.Messages
{
    public class AttemptToStartJob
    {
        public AttemptToStartJob(IStartJob job)
        {
            Job = job;
        }

        public IStartJob Job { get; }
    }
}
