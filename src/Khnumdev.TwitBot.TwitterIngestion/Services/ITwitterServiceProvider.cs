namespace Khnumdev.TwitBot.TwitterIngestion.Services
{
    using System.Threading.Tasks;

    interface ITwitterServiceProvider
    {
        Task LoadIntoDatabaseAsync();
    }
}
