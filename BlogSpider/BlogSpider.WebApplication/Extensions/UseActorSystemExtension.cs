using Akka.Actor;
using Akka.Routing;
using BlogSpider.WebApplication.Actors;
using Microsoft.AspNetCore.Hosting;
using BlogSpider.AkkaConfiguration;

namespace BlogSpider.WebApplication.Extensions
{
    public static class UseActorSystemExtension
    {
        private static ActorSystem ActorSystem;

        public static IWebHostBuilder UseActorSystem(this IWebHostBuilder hostbuilder)
        {
            ActorSystem = Configuration.LaunchSystem("webcrawler", "WebApplication.hocon");
            var router = ActorSystem.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "tasker");
            SystemActors.CommandProcessor = ActorSystem.ActorOf(Props.Create(() => new CommandProcessor(router)), "commands");
            SystemActors.SignalRActor = ActorSystem.ActorOf(Props.Create(() => new SignalRActor()), "signalr");
            return hostbuilder;
        }
    }
}
