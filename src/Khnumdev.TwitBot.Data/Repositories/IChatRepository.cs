namespace Khnumdev.TwitBot.Data.Repositories
{
    using Model;
    using System.Threading.Tasks;

    public interface IChatRepository
    {
        Task EnqeueChatAsync(QueueChat chat);
    }
}
