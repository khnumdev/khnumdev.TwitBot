namespace Khnumdev.TwitBot.TwitterIngestion.Services
{
    using System.Threading.Tasks;

    interface ITwitterAnalyzer
    {
        Task CheckPendingTweets();
    }
}
