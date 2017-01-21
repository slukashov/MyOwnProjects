using Akka.Actor;
using Akka.Configuration;
using System.IO;
using System.Reflection;

namespace BlogSpider.AkkaConfiguration
{
    public static class Configuration
    {
        public static ActorSystem LaunchSystem(string systemName,string fileName)
        {
            var configuration = File.ReadAllText(GetAkkaConfigFilePath(fileName));
            var clusterConfiguration = ConfigurationFactory.ParseString(configuration);
            return ActorSystem.Create(systemName, clusterConfiguration);
        }

        private static string GetAkkaConfigFilePath(string fileName)
        {
            var currentLocation = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(currentLocation);
            if (directory == null)
                throw new FileNotFoundException($"Not found{fileName}config directory");
            return Path.Combine(directory, fileName);
        }
    }
}