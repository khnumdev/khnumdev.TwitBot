namespace Khnumdev.TwitBot.DWHIngestionJob.Startup
{
    using Microsoft.Azure.WebJobs;
    using System;

    class JobFactory
    {
        public static JobHost CreateJob()
        {
            JobHostConfiguration config = new JobHostConfiguration();
            config.Queues.BatchSize = 1;
            config.Queues.MaxDequeueCount = 1;
            config.Queues.MaxPollingInterval = TimeSpan.FromMinutes(5);

            JobHost host = new JobHost(config);

            return host;
        }
    }
}
