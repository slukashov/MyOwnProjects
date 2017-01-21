namespace BlogSpider.WebApplication.Actors.Messages
{
    public class AttemptCrawl
    {
        public AttemptCrawl(string rawString)
        {
            RawString = rawString;
        }

        public string RawString { get; set; }
    }
}
