namespace BlogSpider.Scheduler.Actors.Messages
{
    public class DebugCluster
    {
        public DebugCluster(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
