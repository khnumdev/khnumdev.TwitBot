namespace Khnumdev.TwitBot
{
    using Core.TextAnalyzer.Model;
    using Core.TextAnalyzer.Services;
    using Data.Model;
    using Data.Repositories;
    using Microsoft.Bot.Connector;
    using Services;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    [BotAuthentication]
    public class MessagesController : ApiController
    {
        readonly IMessageMatcherProcessor _messageMatcherProcessor;
        readonly IChatRepository _chatRepository;
        readonly ITextAnalyzerService _textAnalyzerService;

        public MessagesController(IMessageMatcherProcessor messageMatcherProcesor,
            IChatRepository chatRepository,
            ITextAnalyzerService textAnalyzerService)
        {
            _messageMatcherProcessor = messageMatcherProcesor;
            _chatRepository = chatRepository;
            _textAnalyzerService = textAnalyzerService;
        }

        [HttpPost]
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            string chatResponse = null;
            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            try
            {
               
                if (activity.Type == ActivityTypes.Message)
                {
                    var chat = CreateChatFromMessage(activity);
                    var analyzer = new TextAnalyzerService();
                    var analysisResult = await analyzer.AnalyzeAsync(activity.Text);

                    chat.Sentiment = analysisResult.Sentiment;
                    chat.KeyPhrases = analysisResult.KeyPhrases;

                    var selectedResponse = await _messageMatcherProcessor.ProcessAsync(activity.From.Name, analysisResult, activity.Text);

                    chat.Response = selectedResponse.Message;
                    chat.ResponseTime = DateTime.UtcNow;
                    chat.SentimentResponse = selectedResponse.Sentiment;
                    // calculate something for us to return
                    int length = (activity.Text ?? string.Empty).Length;

                    chatResponse = chat.Response;
                    await _chatRepository.QeueChatAsync(chat);
                }

               
            }
            catch (Exception ex)
            {
                chatResponse = ex.ToString();
            }

            // return our reply to the user
            Activity reply = activity.CreateReply(chatResponse);

          
            await connector.Conversations.ReplyToActivityAsync(reply);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        QueueChat CreateChatFromMessage(Activity message)
        {
            return new QueueChat
            {
                RequestTime = DateTime.UtcNow,
                FromId = message.From.Id,
                From = message.From.Name,
                IsRequestFromBot = true,
                RequestAddress = message.From.Name,
                Request = message.Text,
                ToId = message.Recipient.Id,
                TopicName = message.TopicName,
                To = message.Recipient.Name,
                MessageType = message.Type,
                SourceLanguage = message.Locale,
                DestinationLanguage = message.Locale,
                ConversationId = message.Conversation.Id,
                ChannelId = message.ChannelId,
            };
        }
    }
}