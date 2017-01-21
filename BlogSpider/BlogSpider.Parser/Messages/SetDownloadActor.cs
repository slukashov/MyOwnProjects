using Akka.Actor;

namespace BlogSpider.Parser.Messages
{
    public class SetDownloadActor
    {
        public SetDownloadActor(IActorRef downloader)
        {
            Downloader = downloader;
        }

        public IActorRef Downloader { get; }
    }
}
