namespace Khnumdev.TwitBot.TwitterIngestion.Services
{
    using Data.Model;
    using Data.Repositories;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using TweetSharp;

    class TwitterServiceProvider : ITwitterServiceProvider
    {
        const string CONSUMER_KEY = "TwitterConsumerKey";
        const string CONSUMER_SECRET = "TwitterConsumerSecret";
        const string TOKEN = "TwitterToken";
        const string TOKEN_SECRET = "TwitterTokenSecret";
        const string USER_ID = "TwitterUserId";

        readonly TwitterService _twitterService;
        readonly ITwitterRepository _twitterRepository;

        public TwitterServiceProvider(ITwitterRepository twitterRepository)
        {
            _twitterRepository = twitterRepository;

            // Authenticate witht the Twitter API
            _twitterService = new TwitterService(ConfigurationManager.AppSettings[CONSUMER_KEY], ConfigurationManager.AppSettings[CONSUMER_SECRET]);
            _twitterService.AuthenticateWith(ConfigurationManager.AppSettings[TOKEN], ConfigurationManager.AppSettings[TOKEN_SECRET]);
        }

        public async Task LoadIntoDatabaseAsync()
        {
            var userId = long.Parse(ConfigurationManager.AppSettings[USER_ID]);

            var twitterUser = await GetUser(userId);

            var lastTweetId = await _twitterRepository
                .GetLastMessageIdAsync(userId);

            var result = new List<Data.Model.Tweet>();

            var lastTweetIdToFilter = lastTweetId > 0 ? lastTweetId : (long?)null;

            IEnumerable<TwitterStatus> tweets = null;
            tweets = _twitterService.ListTweetsOnUserTimeline(CreateTimeLineOptions(userId, lastTweetIdToFilter, null));

            result.AddRange(tweets
                .Select(i => ParseFromTwitterStatus(twitterUser.Id, i)));

            // Paginate if there are more tweets to load
            while (tweets.Count() > 1)
            {
                var lastId = tweets
                    .Min(i => i.Id);

                tweets = _twitterService.ListTweetsOnUserTimeline(CreateTimeLineOptions(userId, lastTweetIdToFilter, lastId));

                result.AddRange(tweets
                .Select(i => ParseFromTwitterStatus(twitterUser.Id, i)));
            }

            await _twitterRepository.AddAsync(result);
        }

        async Task<Data.Model.TwitterUser> GetUser(long userId)
        {
            Data.Model.TwitterUser user = null;
            var existingUser = await _twitterRepository.GetUserFrom(userId);

            if (existingUser == null)
            {
                var userFromApi = _twitterService.GetUserProfileFor(new GetUserProfileForOptions { UserId = userId });
                user = new Data.Model.TwitterUser
                {
                    TwitterId = userId,
                    TwitterUsername = userFromApi.ScreenName
                };

                await _twitterRepository.AddAsync(user);
            }
            else
            {
                user = existingUser;
            }

            return user;
        }

        Tweet ParseFromTwitterStatus(int userId, TwitterStatus twitterStatus)
        {
            return new Tweet { Text = twitterStatus.Text, TweetId = twitterStatus.Id, TwitterUserId = userId };
        }

        ListTweetsOnUserTimelineOptions CreateTimeLineOptions(long userId, long? sinceId, long? lastId)
        {
            return new ListTweetsOnUserTimelineOptions
            {
                UserId = userId,
                ExcludeReplies = true,
                Count = 1000,
                ContributorDetails = false,
                IncludeRts = false,
                MaxId = lastId,
                SinceId = sinceId
            };
        }
    }
}