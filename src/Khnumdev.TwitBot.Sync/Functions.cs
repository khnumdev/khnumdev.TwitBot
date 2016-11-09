namespace Khnumdev.TwitBot.SyncJob
{
    using Microsoft.Azure.WebJobs;
    using Microsoft.Practices.Unity;
    using Processors;
    using Startup;
    using System.Threading.Tasks;

    public class Functions
    {
        [NoAutomaticTrigger]
        public async static Task ExecuteAsync()
        {
            var unityContainer = UnityResolver.CreateContainer();
            var topicProcessor = unityContainer.Resolve<ITopicProcessor>();
            var chatProcessor = unityContainer.Resolve<IChatIngestionProcessor>();

            await topicProcessor.ProcessAsync();
            await chatProcessor.ProcessAsync();
        }
    }
}
