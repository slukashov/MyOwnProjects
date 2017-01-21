using BlogSpider.Tracking.Service;
using Topshelf;

namespace BlogSpider.Tracking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(configurator =>
            {
                configurator.Service<TrackerService>(service =>
                {
                    service.ConstructUsing(name => new TrackerService());
                    service.WhenStarted((start, hostControl) => start.Start(hostControl));
                    service.WhenStopped((stop, hostControl) => stop.Stop(hostControl));
                });
                configurator.RunAsLocalSystem();
                configurator.SetServiceName("Tracker");
                configurator.SetDisplayName("BlogSpiderTracker");
                configurator.SetDescription(" Cluster  - BlogSpider.");
                configurator.StartAutomatically();
                configurator.EnableServiceRecovery(r => r.RestartService(1));
            });
        }
    }
}
