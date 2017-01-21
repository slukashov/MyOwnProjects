using System;
using BlogSpider.Database.Repository.ReadRepository;
using BlogSpider.Scheduler.Hub;
using MongoDB.Driver;
using Quartz;

namespace BlogSpider.Scheduler.Scheduler.Job
{
    public class SchedulerJob : IJob
    {
        private string GetRandomLink()
        {
            var mongoClient = new MongoClient();
            var repository = new ReadRepository(mongoClient, "Crawler", "CrawlingStats");
            var listOfDocuments = repository.GetAllFromCollection();
            var index = new Random().Next(0, listOfDocuments.Count - 1);
            return listOfDocuments[index][2].ToString();
        }

        public void Execute(IJobExecutionContext context)
        {
            string link;
            while (!string.IsNullOrEmpty(link = GetRandomLink()))
            {
                break;
            }

            if (link != null)
            {
                var uri = new Uri(link);
                Console.WriteLine($"Start recrawling {uri.AbsoluteUri}");
               new CrawlHub().StartCrawl(link);
            }
        }
    }
}