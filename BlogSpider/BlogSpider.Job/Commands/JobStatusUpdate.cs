using BlogSpider.Job.Commands.Interfaces;
using BlogSpider.Job.States;
using System;

namespace BlogSpider.Job.Commands
{
    public enum JobStatus
    {
        Running = 0,
        Starting = 1,
        Failed = 2,
        Finished = 3,
        Stopped = 4
    }

    public class JobStatusUpdate : IStatusUpdate
    {
        public JobStatusUpdate(CrawlJob job)
        {
            Job = job;
            StartTime = DateTime.UtcNow;
            Status = JobStatus.Starting;
        }

        public CrawlJob Job { get; }

        public CrawlJobStats Stats { get; set; }

        public DateTime StartTime { get; }

        public DateTime? EndTime { get; set; }

        public TimeSpan Elapsed => (EndTime ?? DateTime.UtcNow) - StartTime;

        public JobStatus Status { get; set; }
    }
}
