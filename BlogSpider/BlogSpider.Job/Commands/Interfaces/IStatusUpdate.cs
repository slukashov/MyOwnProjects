using BlogSpider.Job.States;
using System;

namespace BlogSpider.Job.Commands.Interfaces
{
    public interface IStatusUpdate
    {
        CrawlJob Job { get; }
        CrawlJobStats Stats { get; set; }
        DateTime StartTime { get; }
        DateTime? EndTime { get; set; }
        TimeSpan Elapsed { get; }
        JobStatus Status { get; set; }
    }
}
