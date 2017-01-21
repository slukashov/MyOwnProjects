using Akka.Actor;

namespace BlogSpider.WebApplication.Actors
{
    public static class SystemActors
    {
        public static IActorRef SignalRActor = ActorRefs.Nobody;
        public static IActorRef CommandProcessor = ActorRefs.Nobody;
    }
}
