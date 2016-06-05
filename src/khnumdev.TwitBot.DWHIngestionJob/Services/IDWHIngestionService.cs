namespace Khnumdev.TwitBot.DWHIngestionJob.Services
{
    using System.Threading.Tasks;

    interface IDWHIngestionService
    {
        Task ProcessMessageAsync(string message);
    }
}
