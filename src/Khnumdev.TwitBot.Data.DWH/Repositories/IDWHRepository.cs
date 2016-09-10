namespace Khnumdev.TwitBot.Data.DWH.Repositories
{
    using Model.Dimensions;
    using System.Threading.Tasks;

    public interface IDWHRepository
    {
        Task<int> AddOrRetrieveIdAsync(Channel entity);

        Task<int> AddOrRetrieveIdAsync(ConversationTrack entity);

        Task<int> AddOrRetrieveIdAsync(Language entity);

        Task<int> AddOrRetrieveIdAsync(MessageSource entity);

        Task<int> AddOrRetrieveIdAsync(MessageType entity);

        Task<int> AddOrRetrieveIdAsync(User entity);

        Task<int> AddOrRetrieveIdAsync(SingleWord entity);

        Task<int> AddAsync<T>(T entity)
            where T : class, IDimension;

        Task AddFactAsync<T>(T entity)
          where T : class;
    }
}