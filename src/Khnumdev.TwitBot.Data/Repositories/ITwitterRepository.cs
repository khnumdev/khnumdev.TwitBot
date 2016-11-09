namespace Khnumdev.TwitBot.Data.Repositories
{
    using Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITwitterRepository
    {
        Task<long> GetLastMessageIdAsync(long userId);

        Task<List<Tweet>> GetTweetContentFromAsync(long userId);

        Task<List<Tweet>> GetTweetContentAsync();

        Task<TwitterUser> GetUserFrom(long userId);

        Task<List<TwitterUser>> GetUsers();

        Task AddAsync(TwitterUser user);

        Task AddAsync(List<Tweet> tweets);

        Task AddAsync(List<TrendingTopic> trendingTopics);

        Task<List<Tweet>> GetPendingTweetsToAnalyzeAsync();

        Task<List<TrendingTopic>> GetNewTrendingTopics();

        Task UpdateAsync(List<Tweet> tweets);

        Task<List<Tweet>> GetTweetsWithoutTopic();
    }
}
