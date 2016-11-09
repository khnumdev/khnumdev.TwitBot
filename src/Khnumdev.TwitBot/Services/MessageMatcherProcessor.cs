namespace Khnumdev.TwitBot.Services
{
    using Core.TextAnalyzer.Model;
    using Data.Model;
    using Data.Repositories;
    using Microsoft.Cognitive.LUIS;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class MessageMatcherProcessor : IMessageMatcherProcessor
    {
        const string LUIS_SubscriptionKey = "CognitiveServicesLUISSubscriptionKey";
        const string LUIS_AppId = "CognitiveServicesLUISAppId";
        const string MESSAGES_TAG = "messages";

        readonly ITwitterRepository _twitterRepository;
        readonly string _luisSubscriptionKey;
        readonly string _luisAppId;
        readonly MemoryCache _memoryCache;

        public MessageMatcherProcessor(ITwitterRepository twitterRepository)
        {
            _twitterRepository = twitterRepository;
            _luisSubscriptionKey = ConfigurationManager.AppSettings[LUIS_SubscriptionKey];
            _luisAppId = ConfigurationManager.AppSettings[LUIS_AppId];
            _memoryCache = MemoryCache.Default;
        }

        public async Task<MatchedMessage> ProcessAsync(string username, AnalysisResult result, string input)
        {
            input = TransformCommandToInput(input);

            List<Tweet> messages = _memoryCache.Get(MESSAGES_TAG) as List<Tweet>;

            if (messages == null)
            {
                messages = await _twitterRepository
                    .GetTweetContentAsync();

                _memoryCache.Add(MESSAGES_TAG, messages, DateTimeOffset.UtcNow.AddHours(1));
            }

            MatchedMessage matchedMessage = ProcessGreetingsMessage(messages, username, result, input);

            if (matchedMessage == null)
            {
                matchedMessage = await PredictWithLUIS(messages, username, input);
            }

            if (matchedMessage == null)
            {
                matchedMessage = ProcessDefaultMessage(messages, username, result, input);
            }

            return matchedMessage;
        }

        /// <summary>
        /// Best match returns 1
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tweetKeyPhrases"></param>
        /// <returns></returns>
        public int CalculatePharseCoincidence(string keyPhrases, string tweetKeyPhrases)
        {
            var splitedTweetPhrases = SanitizeTweet(tweetKeyPhrases, false)
                .ToLowerInvariant()
                .Replace(',', ' ')
                .Split(' ')
                .Distinct();

            var splittedKeyPhrases = keyPhrases.ToLowerInvariant().Split(' ').Distinct();

            var numberOfMatchedWords = splitedTweetPhrases
                .Where(i => splittedKeyPhrases.Contains(i))
                .Count();

            return numberOfMatchedWords;
        }

        string SanitizeTweet(string tweet, bool removeUsername)
        {
            string result = tweet;

            if (removeUsername)
            {
                var regex = @"@(\w+)";
                var regexMatch = new Regex(regex);
                result = regexMatch.Replace(tweet, string.Empty);
            }

            return result.Replace("@", " ")
                .Replace("_", " ")
                .TrimStart();
        }

        async Task<MatchedMessage> PredictWithLUIS(List<Tweet> messages, string username, string input)
        {
            bool _preview = true;
            LuisClient client = new LuisClient(_luisAppId, _luisSubscriptionKey, _preview);
            LuisResult res = await client.Predict(input);
            return ProcessLUISMessage(messages, username, res);
        }
        
        MatchedMessage ProcessGreetingsMessage(List<Tweet> messages, string username, AnalysisResult result, string input)
        {
            MatchedMessage messageResult = null;

            var filteredMessages = messages
               .Where(i => i.Text.StartsWith("Hola @")
               && !i.IsReply);

            if (input.ToUpperInvariant().Contains("HOLA ") ||
                input.ToUpperInvariant().Contains("HOLA,") ||
                input.ToUpperInvariant().StartsWith("HOLA"))
            {
                var random = new Random();

                var message = filteredMessages.ElementAt(random.Next(0, filteredMessages.Count() - 1));

                messageResult = new MatchedMessage
                {
                    Message = ReplaceUsername(message.Text, username),
                    Sentiment = message.Sentiment
                };
            }

            return messageResult;
        }

        MatchedMessage ProcessLUISMessage(List<Tweet> messages, string username, LuisResult res)
        {
            MatchedMessage result = null;
            var topScoreIntent = res.TopScoringIntent;
            var topicToSearch = string.Empty;
            var verbWord = string.Empty;

            var filteredMessages = messages
                .Where(i => !i.Text.Contains("Hola @"));

            topicToSearch = string.Join(" ", res.Entities.SelectMany(i => i.Value).OrderBy(i => i.Score).Select(i => i.Value));

            switch (topScoreIntent.Name)
            {
                case "ConocesA":
                    {
                        verbWord = "conozco";
                        break;
                    }
                case "QueOpinas":
                    {
                        verbWord = "opino";
                        break;
                    }
                case "CualEs":
                    {
                        verbWord = "es";
                        break;
                    }
                case "QuienEs":
                    {
                        verbWord = "es";
                        break;
                    }
                case "TeGusta":
                    {
                        verbWord = "gusta";
                        break;
                    }
                case "EresUnBot":
                    {
                        verbWord = "es un";
                        topicToSearch = "submarinismo";
                        break;
                    }
            }

            if (!string.IsNullOrWhiteSpace(verbWord) && topScoreIntent.Score > 0.5d)
            {
                var selectedMessages = filteredMessages
                    .Where(i => i.Text.ToUpperInvariant().Split(' ', ',', '.', ':', '-').Contains(topicToSearch.ToUpperInvariant()))
                    .ToList();

                var random = new Random();

                if (selectedMessages.Any())
                {
                    var predefinedPhrase = selectedMessages.ElementAt(random.Next(0, selectedMessages.Count()));
                    var phrasesWithVerb = selectedMessages.Where(i => i.Text.ToUpperInvariant().Contains(verbWord)).ToList();

                    if (phrasesWithVerb.Any())
                    {
                        predefinedPhrase = phrasesWithVerb.ElementAt(random.Next(0, phrasesWithVerb.Count()));
                    }

                    result = new MatchedMessage
                    {
                        Message = ReplaceUsername(predefinedPhrase.Text, username),
                        Sentiment = predefinedPhrase.Sentiment
                    };
                }
                else
                {
                    selectedMessages = filteredMessages
                    .Where(i => i.Text.ToUpperInvariant().Contains(verbWord.ToUpperInvariant()))
                    .ToList();

                    if (selectedMessages.Any()
                        && !string.IsNullOrWhiteSpace(topicToSearch))
                    {
                        var predefinedPhrase = selectedMessages.ElementAt(random.Next(0, selectedMessages.Count()));

                        result = new MatchedMessage
                        {
                            Message = ReplaceUsername(predefinedPhrase.Text, username),
                            Sentiment = predefinedPhrase.Sentiment
                        };
                    }
                }
            }

            return result;
        }

        MatchedMessage ProcessDefaultMessage(List<Tweet> messages, string username, AnalysisResult result, string input)
        {
            var random = new Random();

            var keyPhrases = string.Join(" ", result.KeyPhrases);

            var groupedMessages = messages
                .Where(i => i.KeyPhrases != null)
                .Select(i =>
                new
                {
                    Message = i.Text,
                    Sentiment = i.Sentiment,
                    Value = CalculatePharseCoincidence(keyPhrases, i.KeyPhrases)
                })
                .GroupBy(i => i.Value, i => i)
                .OrderByDescending(i => i.Key)
                .First();

            var selectedMessage = groupedMessages.ElementAt(random.Next(0, groupedMessages.Count() - 1));

            return new MatchedMessage
            {
                Message = ReplaceUsername(selectedMessage.Message, username),
                Sentiment = selectedMessage.Sentiment
            };
        }

        string ReplaceUsername(string tweetContent, string username)
        {
            var regex = new Regex(@"@\w+");

            var sanitizedUsername = string.IsNullOrWhiteSpace(username) ? "hamij@" : username;

            return regex.Replace(tweetContent, sanitizedUsername);
        }

        IEnumerable<Tweet> FilterTweetsByContains(IEnumerable<Tweet> tweets, string word)
        {
            return tweets
                    .Where(i => i.Text.ToUpperInvariant().Contains(word.ToUpperInvariant()))
                    .ToList();
        }

        /// <summary>
        /// To prevent Telegram commands as an input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string TransformCommandToInput(string input)
        {
            var result = input;

            if (input == "/start")
            {
                result = "hola";
            }

            return result;
        }
    }
}