namespace Khnumdev.TwitBot.DWHIngestionJob.Startup
{
    using Microsoft.Azure.WebJobs;
    using System;
    using System.Data.Entity;

    class JobFactory
    {
        public static JobHost CreateJob()
        {
            JobHostConfiguration config = new JobHostConfiguration();
            config.Queues.BatchSize = 1;
            config.Queues.MaxDequeueCount = 1;
            config.Queues.MaxPollingInterval = TimeSpan.FromMinutes(5);
            DbConfiguration.SetConfiguration(new DatabaseConfiguration());
            JobHost host = new JobHost(config);

            return host;
        }
    }
}
