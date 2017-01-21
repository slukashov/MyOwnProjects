using BlogSpider.Scheduler.Scheduler.Service;
using Topshelf;

namespace BlogSpider.Scheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(configurator =>
            {
                configurator.Service<SchedulerService>(service =>
                {
                    service.ConstructUsing(name => new SchedulerService());
                    service.WhenStarted((start, hostControl) => start.Start(hostControl));
                    service.WhenStopped((stop, hostControl) => stop.Stop(hostControl));
                });
                configurator.RunAsLocalSystem();
                configurator.SetServiceName("Scheduler");
                configurator.SetDisplayName("BlogSpiderScheduler");
                configurator.SetDescription(" Cluster  - BlogSpider.Scheduler");
                configurator.StartAutomatically();
                configurator.EnableServiceRecovery(r => r.RestartService(1));
            });
        }
    }
}
