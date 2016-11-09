namespace Khnumdev.TwitBot.Data.Repositories
{
    using Model;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System;

    public class TwitterRepository : ITwitterRepository
    {
        public async Task<long> GetLastMessageIdAsync(long userId)
        {
            using (var context = new TwitBotContext())
            {
                return await context
                    .Set<Tweet>()
                    .Where(i => i.TwitterUser.TwitterId == userId)
                    .Select(i => i.TweetId)
                    .DefaultIfEmpty()
                    .MaxAsync(i => i);
            }
        }

        public async Task<List<Tweet>> GetTweetContentFromAsync(long userId)
        {
            using (var context = new TwitBotContext())
            {
                var tweets = await context
                    .Set<Tweet>()
                    .Where(i => i.TwitterUser.TwitterId == userId)
                    .Select(i => new
                    {
                        Text = i.Text,
                        KeyPhrases = i.KeyPhrases,
                        Sentiment = i.Sentiment
                    })
                    .ToListAsync();

                return tweets
                    .Select(i => new Tweet
                    {
                        KeyPhrases = i.KeyPhrases,
                        Text = i.Text,
                        Sentiment = i.Sentiment
                    })
                    .ToList();
            }
        }

        public async Task<List<Tweet>> GetTweetContentAsync()
        {
            using (var context = new TwitBotContext())
            {
                var tweets = await context
                    .Set<Tweet>()
                    .Select(i => new
                    {
                        Text = i.Text,
                        KeyPhrases = i.KeyPhrases,
                        Sentiment = i.Sentiment
                    })
                    .ToListAsync();

                return tweets
                    .Select(i => new Tweet
                    {
                        KeyPhrases = i.KeyPhrases,
                        Text = i.Text,
                        Sentiment = i.Sentiment
                    })
                    .ToList();
            }
        }

        public async Task<TwitterUser> GetUserFrom(long userId)
        {
            using (var context = new TwitBotContext())
            {
                return await context
                    .Set<TwitterUser>()
                    .Where(i => i.TwitterId == userId)
                    .SingleOrDefaultAsync();
            }
        }

        public async Task<List<TwitterUser>> GetUsers()
        {
            using (var context = new TwitBotContext())
            {
                return await context
                    .Set<TwitterUser>()
                    .ToListAsync();
            }
        }

        public async Task AddAsync(TwitterUser user)
        {
            using (var context = new TwitBotContext())
            {
                var existingEntity = await context
                     .Set<TwitterUser>()
                     .Where(i => i.TwitterId == user.TwitterId)
                     .SingleOrDefaultAsync();

                if (existingEntity == null)
                {
                    context
                     .Set<TwitterUser>()
                     .Add(user);

                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task AddAsync(List<Tweet> tweets)
        {
            using (var context = new TwitBotContext())
            {
                var existingIds = await context
                     .Set<Tweet>()
                     .Select(i => i.TweetId)
                     .ToListAsync();

                tweets
                    .Where(i => !existingIds.Contains(i.TweetId))
                    .Select(i =>
                   {
                       context
                        .Set<Tweet>()
                        .Add(i);

                       return i;
                   })
                   .ToList();

                await context.SaveChangesAsync();
            }
        }

        public async Task AddAsync(List<TrendingTopic> trendingTopics)
        {
            using (var context = new TwitBotContext())
            {
                context.Set<TrendingTopic>().AddRange(trendingTopics);

                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Tweet>> GetPendingTweetsToAnalyzeAsync()
        {
            using (var context = new TwitBotContext())
            {
                var tweets = await context
                     .Set<Tweet>()
                     .Where(i => i.KeyPhrases == null && !i.Sentiment.HasValue)
                     .Take(100)
                     .ToListAsync();

                return tweets;
            }
        }

        public async Task UpdateAsync(List<Tweet> tweets)
        {
            using (var context = new TwitBotContext())
            {
                foreach (var tweet in tweets)
                {
                    context.Entry<Tweet>(tweet).State = EntityState.Modified;
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<List<TrendingTopic>> GetNewTrendingTopics()
        {
            var endDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, 0, 0);
            var initialDate = endDate.AddHours(-1);

            using (var context = new TwitBotContext())
            {
                var tweets = await context
                     .Set<TrendingTopic>()
                     .Where(i => i.Date >= initialDate && i.Date < endDate)
                     .ToListAsync();

                return tweets;
            }
        }

        public async Task<List<Tweet>> GetTweetsWithoutTopic()
        {
            using (var context = new TwitBotContext())
            {
                var tweets = await context
                     .Set<Tweet>()
                     .ToListAsync();

                return tweets;
            }
        }
    }
}
