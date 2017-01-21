using System;
using System.Threading.Tasks;
using BlogSpider.Scheduler.Scheduler.Job;
using Quartz;
using Quartz.Impl;

namespace BlogSpider.Scheduler.Scheduler
{
    public class Sсheduler
    {
        #region Constants
        private const int DayIntervalInHours = 24;
        private const int DaysInWeek = 7;
        public const int WeekIntervalInHours = DaysInWeek * DayIntervalInHours;
        #endregion
        public static async Task Run()
        {
            try
            {
                var scheduler = StdSchedulerFactory.GetDefaultScheduler();
                scheduler.Start();
                var job = JobBuilder.Create<SchedulerJob>()
                    .WithIdentity("Recrawl", "CrawlerGroup")
                    .Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity("crawlTrigger", "CrawlerGroup")
                    .StartNow()
                    .WithSimpleSchedule(scheduleBuilder => scheduleBuilder
                        .WithIntervalInHours(WeekIntervalInHours)
                        .RepeatForever())
                    .Build();

                scheduler.ScheduleJob(job, trigger);
                await Task.Delay(TimeSpan.FromSeconds(60));
                scheduler.Shutdown();
            }
            catch (SchedulerException schedulerException)
            {
                Console.WriteLine(schedulerException);
            }
        }
    }
}