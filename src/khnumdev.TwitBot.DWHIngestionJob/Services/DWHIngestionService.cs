namespace Khnumdev.TwitBot.DWHIngestionJob.Services
{
    using Data.DWH.Helpers;
    using Data.DWH.Model.Dimensions;
    using Data.DWH.Model.Facts;
    using Data.DWH.Repositories;
    using Data.Model;
    using Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class DWHIngestionService
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
            var messageSource = GetMessageSourceFrom(chat);
            var messageType = GetMessageTypeFrom(chat);
            var userFrom = GetUserFrom(chat);
            var userTo = GetUserTo(chat);

            var channelId = await _dwhRepository.AddOrRetrieveIdAsync(channel);
            var conversationId = await _dwhRepository.AddOrRetrieveIdAsync(conversationTrack);
            var sourceLanguageId = await _dwhRepository.AddOrRetrieveIdAsync(sourceLanguage);
            var requestMessageId = await _dwhRepository.AddAsync(requestMessage);
            var responseMessageId = await _dwhRepository.AddAsync(responseMessage);
            var messageTypeId = await _dwhRepository.AddAsync(messageType);
            var messageSourceId = await _dwhRepository.AddOrRetrieveIdAsync(messageSource);
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
                FromUserId = userFromId,
                MessageId = responseMessageId,
                MessageTypeId = messageTypeId,
                MessageSourceId = 2,
                ToUserId = userToId
            };

            await _dwhRepository.AddFactAsync(requestMessage);
            await _dwhRepository.AddFactAsync(responseConversation);
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

        MessageSource GetMessageSourceFrom(QueueChat chat)
        {
            return new MessageSource
            {
                Source = chat.IsRequestFromBot.HasValue ?
                chat.IsRequestFromBot.Value ? "Response" : "Request" : string.Empty
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
