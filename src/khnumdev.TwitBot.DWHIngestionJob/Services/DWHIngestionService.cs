namespace Khnumdev.TwitBot.DWHIngestionJob.Services
{
    using Data.DWH.Helpers;
    using Data.DWH.Model.Dimensions;
    using Data.DWH.Model.Facts;
    using Data.DWH.Repositories;
    using Data.Model;
    using Data.Repositories;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    class DWHIngestionService: IDWHIngestionService
    {
        readonly IChatRepository _chatRepository;
        readonly IDWHRepository _dwhRepository;

        public DWHIngestionService(IChatRepository chatRepository, IDWHRepository dwhRepository)
        {
            _chatRepository = chatRepository;
            _dwhRepository = dwhRepository;
        }

        public async Task ProcessMessageAsync(string message)
        {
            var chat = _chatRepository.DeserializeFrom(message);

            // Get dimension values
            var channel = GetChannelFrom(chat);
            var conversationTrack = GetConversationTrackFrom(chat);
            var sourceLanguage = GetSourceLanguageFrom(chat);
            var requestMessage = GetRequestMessageFrom(chat);
            var responseMessage = GetRequestMessageTo(chat);
            var messageType = GetMessageTypeFrom(chat);
            var userFrom = GetUserFrom(chat);
            var userTo = GetUserTo(chat);

            var channelId = await _dwhRepository.AddOrRetrieveIdAsync(channel);
            var conversationId = await _dwhRepository.AddOrRetrieveIdAsync(conversationTrack);
            var sourceLanguageId = await _dwhRepository.AddOrRetrieveIdAsync(sourceLanguage);
            var requestMessageId = await _dwhRepository.AddAsync(requestMessage);
            var responseMessageId = await _dwhRepository.AddAsync(responseMessage);
            var messageTypeId = await _dwhRepository.AddOrRetrieveIdAsync(messageType);
            var userFromId = await _dwhRepository.AddOrRetrieveIdAsync(userFrom);
            var userToId = await _dwhRepository.AddOrRetrieveIdAsync(userTo);

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
            await ProcessWords(chat.Request, channelId, conversationId, chat.RequestTime, userFromId, requestMessageId, 1, messageTypeId, userToId);
            await ProcessWords(chat.Response, channelId, conversationId, chat.ResponseTime, userToId, responseMessageId, 2, messageTypeId, userFromId);
        }

        async Task ProcessWords(string requestText, int channelId, int conversationTrackId, DateTime messageTime, int fromUserId, int messageId,
            int messageSourceId, int messageTypeId, int toUserId)
        {
            var words = requestText
                .Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => new Data.DWH.Model.Dimensions.SingleWord
                {
                    Text = i
                })
                .ToList();

            //TODO: Optimize!!!! Use batches instead of getting one by one!
            foreach (var word in words)
            {
                var wordId = await _dwhRepository.AddOrRetrieveIdAsync(word);

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

        Channel GetChannelFrom(QueueChat message)
        {
            return new Channel
            {
                Name = message.ChannelId
            };
        }

        ConversationTrack GetConversationTrackFrom(QueueChat message)
        {
            return new ConversationTrack
            {
                ConversationId = message.ConversationId
            };
        }

        Language GetSourceLanguageFrom(QueueChat chat)
        {
            return new Language
            {
                Name = chat.SourceLanguage
            };
        }

        Language GetDestinationLanguageFrom(QueueChat chat)
        {
            return new Language
            {
                Name = chat.DestinationLanguage
            };
        }

        Message GetRequestMessageFrom(QueueChat chat)
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

        Message GetRequestMessageTo(QueueChat chat)
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

        MessageType GetMessageTypeFrom(QueueChat chat)
        {
            return new MessageType
            {
                Name = chat.MessageType
            };
        }

        User GetUserFrom(QueueChat chat)
        {
            return new User
            {
                Name = chat.From
            };
        }

        User GetUserTo(QueueChat chat)
        {
            return new User
            {
                Name = chat.To
            };
        }
    }
}
