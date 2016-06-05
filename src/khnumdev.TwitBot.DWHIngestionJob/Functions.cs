namespace khnumdev.TwitBot.DWHIngestionJob
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs;
    using Khnumdev.TwitBot.DWHIngestionJob.Startup;
    using Khnumdev.TwitBot.DWHIngestionJob.Services;
    using Microsoft.Practices.Unity;

    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("chat")] string message, TextWriter log)
        {
            try
            {
                var unityContainer = UnityResolver.CreateContainer();
                var service = unityContainer.Resolve<IDWHIngestionService>();
                var serviceTask = service.ProcessMessageAsync(message);
                serviceTask.Wait();

            }
            catch (Exception ex)
            {
                log.WriteLine(ex);
            }
        }
    }
}
