using Topshelf;

namespace BlogSpider.Lighthouse
{
    public class Program
    {
        public static int Main(string[] args)
        {
            return (int)HostFactory.Run(configure =>
             {
                 configure.SetServiceName("Lighthouse");
                 configure.SetDisplayName("Lighthouse Service ");
                 configure.SetDescription("Lighthouse Service for Cluster");

                 configure.UseAssemblyInfoForServiceInfo();
                 configure.RunAsLocalSystem();
                 configure.Service<LighthouseService>();
                 configure.EnableServiceRecovery(configurator => configurator.RestartService(1));
             });
        }
    }
}
