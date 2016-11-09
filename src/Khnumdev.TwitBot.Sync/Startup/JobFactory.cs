namespace Khnumdev.TwitBot.SyncJob.Startup
{
    using Microsoft.Azure.WebJobs;
    using Sync.Startup;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class JobFactory
    {
        public static JobHost CreateJob()
        {
            JobHostConfiguration config = new JobHostConfiguration();
            config.Queues.BatchSize = 10;
            config.Queues.MaxDequeueCount = 1;
            config.Queues.MaxPollingInterval = TimeSpan.FromMinutes(5);

            DbConfiguration.SetConfiguration(new DatabaseConfiguration());

            JobHost host = new JobHost(config);

            return host;
        }
    }
}
