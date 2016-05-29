namespace Khnumdev.TwitBot.TwitterIngestion
{
    using Services;
    using Startup;
    using Microsoft.Azure.WebJobs;
    using System.Threading.Tasks;
    using Microsoft.Practices.Unity;

    public class Functions
    {
        [NoAutomaticTrigger]
        public async static Task ExecuteAsync()
        {
            var unityContainer = UnityResolver.CreateContainer();
            var twitterServiceProvider = unityContainer.Resolve<ITwitterServiceProvider>();
            var twitterAnalyzer = unityContainer.Resolve<ITwitterAnalyzer>();

            await twitterServiceProvider.LoadIntoDatabaseAsync();
            await twitterAnalyzer.CheckPendingTweets();
        }
    }
}
