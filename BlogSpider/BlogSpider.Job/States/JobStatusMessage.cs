namespace BlogSpider.Job.States
{
    public class JobStatusMessage
    {
        public JobStatusMessage(CrawlJob job, string documentTitle, string message)
        {
            Message = message;
            DocumentTitle = documentTitle;
            Job = job;
        }

        public CrawlJob Job { get; }

        public string DocumentTitle { get; }

        public string Message { get; }

        public override string ToString()
        {
            return $"[{Job}][{DocumentTitle}][{Message}]";
        }
    }
}
