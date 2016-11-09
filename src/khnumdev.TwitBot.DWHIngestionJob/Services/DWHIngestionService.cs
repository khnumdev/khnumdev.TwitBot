namespace Khnumdev.TwitBot.DWHIngestionJob.Services
{
    using Data.DWH.Model.Staging;
    using Data.DWH.Repositories;
    using Data.Model;
    using Data.Repositories;
    using System;
    using System.Threading.Tasks;

    class DWHIngestionService : IDWHIngestionService
    {
        readonly IChatRepository _chatRepository;
        readonly IStagingRepository _stagingRepository;
        readonly IConfigurationRepository _configurationRepository;

        public DWHIngestionService(IChatRepository chatRepository, IStagingRepository stagingRepository,
            IConfigurationRepository configurationRepository)
        {
            _chatRepository = chatRepository;
            _stagingRepository = stagingRepository;
            _configurationRepository = configurationRepository;
        }

        public async Task ProcessMessageAsync(string message)
        {
            await CheckState();

            var chat = _chatRepository.DeserializeFrom(message);

            var rawMessage = ParseMessage(chat);

            await _stagingRepository.AddAsync(rawMessage);
        }

        async Task CheckState()
        {
            var state = await _configurationRepository.GetStateByIdAsync(1);

            if (!state.IsEnabled)
            {
                throw new InvalidOperationException(string.Format("The staging table is not available because the state 1 is {0}", state.IsEnabled));
            }
        }
        RAWMessage ParseMessage(QueueChat chat)
        {
            return new RAWMessage
            {
                ChannelId = chat.ChannelId,
                ConversationId = chat.ConversationId,
                DestinationLanguage = chat.DestinationLanguage,
                Error = chat.Error,
                FromId = chat.FromId,
                From = chat.From,
                IsRequestFromBot = chat.IsRequestFromBot,
                MessageType = chat.MessageType,
                Request = chat.Request,
                RequestAddress = chat.RequestAddress,
                RequestTime = chat.RequestTime,
                Response = chat.Response,
                ResponseTime = chat.ResponseTime,
                Sentiment = chat.Sentiment,
                SentimentResponse = chat.SentimentResponse,
                SourceLanguage = chat.SourceLanguage,
                To = chat.To,
                ToId = chat.ToId,
                TopicName = chat.TopicName
            };
        }
    }
}
