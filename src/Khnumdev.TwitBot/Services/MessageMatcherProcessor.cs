namespace Khnumdev.TwitBot.Services
{
    using Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;

    public class MessageMatcherProcessor : IMessageMatcherProcessor
    {
        const string USER_ID = "TwitterUserId";

        static List<string> _messages;

        readonly ITwitterRepository _twitterRepository;
        readonly long _userId;

        public MessageMatcherProcessor(ITwitterRepository twitterRepository)
        {
            _twitterRepository = twitterRepository;
            _userId = long.Parse(ConfigurationManager.AppSettings[USER_ID]);
        }

        public async Task<string> ProcessAsync(string input)
        {
            if (_messages == null)
            {
                _messages = await _twitterRepository
                    .GetMessagesFromAsync(_userId);
            }

            var random = new Random();

            var messages = _messages
                .Where(i => i.ToLowerInvariant().Contains(input.ToLowerInvariant()))
                .ToList();

            return messages.Any() ? messages[random.Next(0, messages.Count)] : "traca";

        }
    }
}