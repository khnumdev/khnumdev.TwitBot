namespace Khnumdev.TwitBot.SyncJob.Processors
{
    using System.Threading.Tasks;

    interface IChatIngestionProcessor
    {
        Task ProcessAsync();
    }
}
