namespace Khnumdev.TwitBot.Data.DWH.Repositories
{
    using Model.Dimensions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDWHRepository
    {
        Task UpsertAsync(List<Channel> entities);

        Task UpsertAsync(List<ConversationTrack> entities);

        Task UpsertAsync(List<Language> entities);

        Task UpsertAsync(List<MessageSource> entities);

        Task UpsertAsync(List<MessageType> entities);

        Task UpsertAsync(List<User> entities);

        Task UpsertAsync(List<SingleWord> entities);

        Task UpsertAsync(List<TrendingTopic> entities);

        Task UpsertAsync(List<Geography> entities);

        Task<int> AddAsync<T>(T entity)
            where T : class, IDimension;

        Task<List<T>> GetAllAsync<T>()
           where T : class, IDimension;

        Task AddFactAsync<T>(T entity)
          where T : class;
    }
}