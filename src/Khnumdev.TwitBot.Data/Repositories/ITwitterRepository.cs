namespace Khnumdev.TwitBot.Data.Repositories
{
    using Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITwitterRepository
    {
        Task<long> GetLastMessageIdAsync(long userId);

        Task<List<Tweet>> GetTweetContentFromAsync(long userId);

        Task<TwitterUser> GetUserFrom(long userId);

        Task AddAsync(TwitterUser user);

        Task AddAsync(List<Tweet> tweets);

        Task<List<Tweet>> GetPendingTweetsToAnalyzeAsync();

        Task UpdateAsync(List<Tweet> tweets);
    }
}
