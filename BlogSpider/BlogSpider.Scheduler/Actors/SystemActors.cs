using Akka.Actor;

namespace BlogSpider.Scheduler.Actors
{
    public static class SystemActors
    {
        public static IActorRef ReCrawlActor = ActorRefs.Nobody;
        public static IActorRef CommandProcessor = ActorRefs.Nobody;
    }
}