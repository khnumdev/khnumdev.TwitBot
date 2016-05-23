namespace Khnumdev.TwitBot.TwitterIngestion
{
    using Startup;
    using System;

    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            try
            {
                var host = JobFactory.CreateJob();
                host.CallAsync(typeof(Functions).GetMethod("ExecuteAsync")).Wait();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
