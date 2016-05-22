namespace Khnumdev.TwitBot.Services
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using TweetSharp;

    class TwitterServiceProvider
    {
        const string CONSUMER_KEY = "TwitterConsumerKey";
        const string CONSUMER_SECRET = "TwitterConsumerSecret";
        const string TOKEN = "TwitterToken";
        const string TOKEN_SECRET = "TwitterTokenSecret";
        const string USER_ID = "TwitterUserId";

        readonly TwitterService _twitterService;

        public TwitterServiceProvider()
        {
            // Authenticate witht the Twitter API
            _twitterService = new TwitterService(ConfigurationManager.AppSettings[CONSUMER_KEY], ConfigurationManager.AppSettings[CONSUMER_SECRET]);
            _twitterService.AuthenticateWith(ConfigurationManager.AppSettings[TOKEN], ConfigurationManager.AppSettings[TOKEN_SECRET]);
        }

        public List<string> GetTweetsFrom(string username)
        {
            List<string> result = new List<string>();

            var userId = long.Parse(ConfigurationManager.AppSettings[USER_ID]);

            IEnumerable<TwitterStatus> tweets = null;
            tweets = _twitterService.ListTweetsOnUserTimeline(CreateTimeLineOptions(userId, null));

            result.AddRange(
                tweets
                .Select(i => i.Text)
                .ToList());

            // Paginate if there are more tweets to load
            while (tweets.Count() > 1)
            {
                var lastId = tweets
                    .Min(i => i.Id);

                tweets = _twitterService.ListTweetsOnUserTimeline(CreateTimeLineOptions(userId, lastId));

                result.AddRange(
                    tweets
                    .Select(i => i.Text)
                    .ToList());
            }

            return result;
        }

        ListTweetsOnUserTimelineOptions CreateTimeLineOptions(long userId, long? lastId)
        {
            return new ListTweetsOnUserTimelineOptions
            {
                UserId = userId,
                ExcludeReplies = true,
                Count = 1000,
                ContributorDetails = false,
                IncludeRts = false,
                MaxId = lastId
            };
        }
    }
}