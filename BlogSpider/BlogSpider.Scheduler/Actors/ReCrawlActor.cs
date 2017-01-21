using System;
using Akka.Actor;
using BlogSpider.Job.Commands.Interfaces;
using BlogSpider.Scheduler.Actors.Messages;

namespace BlogSpider.Scheduler.Actors
{
    public class ReCrawlActor : ReceiveActor
    {
        public ReCrawlActor()
        {
            Receive<string>(str =>
            {
                SystemActors.CommandProcessor.Tell(new AttemptCrawl(str));
            });

            Receive<BadCrawlAttempt>(bad =>
            {
                Console.WriteLine($"COULD NOT CRAWL {bad.RawString}: {bad.Message}");
            });

            Receive<IStatusUpdate>(status =>
            {
                Console.WriteLine(status);
            });

            Receive<IStartJob>(start =>
            {
                Console.WriteLine($"Starting crawl of {start.Job.Root.ToString()}");
            });

            Receive<DebugCluster>(debug =>
            {
                Console.WriteLine($"DEBUG: { debug.Message}");
            });
        }
    }
}
