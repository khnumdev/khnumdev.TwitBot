namespace Khnumdev.TwitBot.SyncJob.Processors
{
    using System.Threading.Tasks;

    interface ITopicProcessor
    {
        Task ProcessAsync();
    }
}
