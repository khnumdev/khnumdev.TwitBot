namespace Khnumdev.TwitBot.SyncJob.Processors
{
    using Data.DWH.Helpers;
    using Data.DWH.Model.Dimensions;
    using Data.DWH.Model.Facts;
    using Data.DWH.Model.Staging;
    using Data.DWH.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    class ChatIngestionProcessor : IChatIngestionProcessor
    {
        readonly IStagingRepository _stagingRepository;
        readonly IDWHRepository _dwhRepository;
        readonly IConfigurationRepository _configurationRepository;

        public ChatIngestionProcessor(IStagingRepository stagingRepository, IDWHRepository dwhRepository, IConfigurationRepository configurationRepository)
        {
            _stagingRepository = stagingRepository;
            _dwhRepository = dwhRepository;
            _configurationRepository = configurationRepository;
        }

        public async Task ProcessAsync()
        {
            var state = await _configurationRepository.GetStateByIdAsync(1);

            try
            {
                state.IsEnabled = false;
                await _configurationRepository.UpdateStateAsync(state);

                // Wait 10 seconds to ensure that there are not any process working on the staging table
                await Task.Delay(10000);

                var messages = await _stagingRepository.GetAsync();

                await ProcessDimensionsAsync(messages);
                await ProcessFactTablesAsync(messages);

                await _stagingRepository.TruncateTableAsync();
            }
            finally
            {
                state.IsEnabled = true;
                await _configurationRepository.UpdateStateAsync(state);
            }
        }

        async Task ProcessFactTablesAsync(List<RAWMessage> messages)
        {
            var channels = await _dwhRepository.GetAllAsync<Channel>();
            var conversations = await _dwhRepository.GetAllAsync<ConversationTrack>();
            var languages = await _dwhRepository.GetAllAsync<Language>();
            var messageTypes = await _dwhRepository.GetAllAsync<MessageType>();
            var users = await _dwhRepository.GetAllAsync<User>();
            var words = await _dwhRepository.GetAllAsync<SingleWord>();

            var indexedChannels = channels.ToDictionary(i => i.Name, i => i);
            var indexedConversations = conversations.ToDictionary(i => i.ConversationId, i => i);
            var indexedLanguages = languages.ToDictionary(i => i.Name, i => i);
            var indexedMessageTypes = messageTypes.ToDictionary(i => i.Name, i => i);
            var indexedUsers = users.ToDictionary(i => i.UserId, i => i);
            var indexedWords = words.ToDictionary(i => i.Text, i => i);

            foreach (var chat in messages)
            {
                // Get dimension values
                var channel = GetChannelFrom(chat);
                var conversationTrack = GetConversationTrackFrom(chat);
                var sourceLanguage = GetSourceLanguageFrom(chat);
                var requestMessage = GetRequestMessageFrom(chat);
                var responseMessage = GetRequestMessageTo(chat);
                var messageType = GetMessageTypeFrom(chat);
                var userFrom = GetUserFrom(chat);
                var userTo = GetUserTo(chat);

                var channelId = indexedChannels[channel.Name].Id;
                var conversationId = indexedConversations[conversationTrack.ConversationId].Id;
                var sourceLanguageId = string.IsNullOrWhiteSpace(sourceLanguage.Name) ? -1 : indexedLanguages[sourceLanguage.Name].Id;
                var requestMessageId = await _dwhRepository.AddAsync(requestMessage);
                var responseMessageId = await _dwhRepository.AddAsync(responseMessage);
                var messageTypeId = indexedMessageTypes[messageType.Name].Id;
                var userFromId = indexedUsers[userFrom.UserId].Id;
                var userToId = indexedUsers[userTo.UserId].Id;

                var requestConversation = new Conversation
                {
                    ChannelId = channelId,
                    ConversationTrackId = conversationId,
                    DateId = DateHelper.GetKeyFromDate(chat.RequestTime),
                    FromUserId = userFromId,
                    MessageId = requestMessageId,
                    MessageTypeId = messageTypeId,
                    MessageSourceId = 1,
                    Sentiment = chat.Sentiment,
                    ToUserId = userToId
                };

                var responseConversation = new Conversation
                {
                    ChannelId = channelId,
                    ConversationTrackId = conversationId,
                    DateId = DateHelper.GetKeyFromDate(chat.ResponseTime),
                    FromUserId = userToId,
                    MessageId = responseMessageId,
                    MessageTypeId = messageTypeId,
                    MessageSourceId = 2,
                    ToUserId = userFromId,
                    Sentiment = chat.SentimentResponse
                };

                await _dwhRepository.AddFactAsync(requestConversation);
                await _dwhRepository.AddFactAsync(responseConversation);

                // Words fact table
                await ProcessWordsAsync(indexedWords, chat.Request, channelId, conversationId, chat.RequestTime, userFromId, requestMessageId, 1, messageTypeId, userToId);
                await ProcessWordsAsync(indexedWords, chat.Response, channelId, conversationId, chat.ResponseTime, userToId, responseMessageId, 2, messageTypeId, userFromId);
            }
        }

        async Task ProcessWordsAsync(Dictionary<string, SingleWord> indexedWords, string requestText, int channelId, int conversationTrackId, DateTime messageTime, int fromUserId, int messageId,
            int messageSourceId, int messageTypeId, int toUserId)
        {
            var words = requestText
                .Split(new string[] { " ", ",", "." }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => new Data.DWH.Model.Dimensions.SingleWord
                {
                    Text = SaniziteWord(i)
                })
                .ToList();

            foreach (var word in words)
            {
                var wordId = indexedWords[word.Text].Id;

                var fact = new Data.DWH.Model.Facts.Word
                {
                    ChannelId = channelId,
                    ConversationTrackId = conversationTrackId,
                    DateId = DateHelper.GetKeyFromDate(messageTime),
                    FromUserId = fromUserId,
                    MessageId = messageId,
                    MessageSourceId = messageSourceId,
                    MessageTypeId = messageTypeId,
                    ToUserId = toUserId,
                    WordId = wordId
                };

                await _dwhRepository.AddFactAsync(fact);
            }
        }

        Channel GetChannelFrom(RAWMessage message)
        {
            return new Channel
            {
                Name = message.ChannelId
            };
        }

        ConversationTrack GetConversationTrackFrom(RAWMessage message)
        {
            return new ConversationTrack
            {
                ConversationId = message.ConversationId
            };
        }

        Language GetSourceLanguageFrom(RAWMessage chat)
        {
            return new Language
            {
                Name = chat.SourceLanguage
            };
        }

        Language GetDestinationLanguageFrom(RAWMessage chat)
        {
            return new Language
            {
                Name = chat.DestinationLanguage
            };
        }

        Message GetRequestMessageFrom(RAWMessage chat)
        {
            return new Message
            {
                ChannelId = chat.ChannelId,
                ConversationId = chat.ConversationId,
                Content = chat.Request,
                Date = chat.RequestTime,
                LoadedOn = DateTime.UtcNow,
                User = chat.From
            };
        }

        Message GetRequestMessageTo(RAWMessage chat)
        {
            return new Message
            {
                ChannelId = chat.ChannelId,
                ConversationId = chat.ConversationId,
                Content = chat.Response,
                Date = chat.ResponseTime,
                LoadedOn = DateTime.UtcNow,
                User = chat.To
            };
        }

        MessageType GetMessageTypeFrom(RAWMessage chat)
        {
            return new MessageType
            {
                Name = chat.MessageType
            };
        }

        User GetUserFrom(RAWMessage chat)
        {
            return new User
            {
                Name = chat.From,
                UserId = chat.FromId
            };
        }

        User GetUserTo(RAWMessage chat)
        {
            return new User
            {
                Name = chat.To,
                UserId = chat.ToId
            };
        }

        string SaniziteWord(string word)
        {
            //TODO: Better with a regex expression...
            return word.Replace("¿", string.Empty)
                .Replace("?", string.Empty)
                .Replace("!", string.Empty)
                .Replace("¡", string.Empty);
        }

        async Task ProcessDimensionsAsync(List<RAWMessage> message)
        {
            var channels = message.Select(chat => GetChannelFrom(chat)).ToList();
            var conversationTracks = message.Select(chat => GetConversationTrackFrom(chat)).ToList();
            var languages = message.Where(i => !string.IsNullOrWhiteSpace(i.SourceLanguage))
                .Select(chat => GetSourceLanguageFrom(chat)).ToList();
            var messageTypes = message.Select(chat => GetMessageTypeFrom(chat)).ToList();
            var userFroms = message.Select(chat => GetUserFrom(chat)).ToList();
            var userTos = message.Select(chat => GetUserTo(chat)).ToList();
            var requestWords = message.SelectMany(chat => chat.Request
               .Split(new string[] { " ", ",", "." }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => new Data.DWH.Model.Dimensions.SingleWord
                {
                    Text = SaniziteWord(i)
                })
                .ToList()).ToList();
            var responseWords = message.SelectMany(chat => chat.Response
               .Split(new string[] { " ", ",", "." }, StringSplitOptions.RemoveEmptyEntries)
               .Select(i => new Data.DWH.Model.Dimensions.SingleWord
               {
                   Text = SaniziteWord(i)
               })
               .ToList()).ToList();

            var wordsToUpsert = requestWords.Concat(responseWords).ToList();
            var usersToUpsert = userTos.Concat(userFroms).ToList();

            await _dwhRepository.UpsertAsync(channels);
            await _dwhRepository.UpsertAsync(conversationTracks);
            await _dwhRepository.UpsertAsync(languages);
            await _dwhRepository.UpsertAsync(messageTypes);
            await _dwhRepository.UpsertAsync(usersToUpsert);
            await _dwhRepository.UpsertAsync(wordsToUpsert);
        }
    }
}
