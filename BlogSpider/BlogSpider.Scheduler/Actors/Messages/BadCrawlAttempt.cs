namespace BlogSpider.Scheduler.Actors.Messages
{
    public class BadCrawlAttempt
    {
        public BadCrawlAttempt(string rawString, string message)
        {
            Message = message;
            RawString = rawString;
        }

        public string RawString { get; }
        public string Message { get; }
    }
}