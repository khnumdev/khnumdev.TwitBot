namespace Khnumdev.TwitBot
{
    using Core.TextAnalyzer.Model;
    using Core.TextAnalyzer.Services;
    using Data.Model;
    using Data.Repositories;
    using Microsoft.Bot.Connector;
    using Services;
    using System;
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
        public async Task<Message> Post([FromBody]Message message)
        {
            var chat = CreateChatFromMessage(message);

            Message response = null;

            try
            {
                if (message.Type == "Message")
                {
                    var analyzer = new TextAnalyzerService();
                    var analysisResult = await analyzer.AnalyzeAsync(message.Text);

                    chat.Sentiment = analysisResult.Sentiment;
                    chat.KeyPhrases = analysisResult.KeyPhrases;

                    var selectedResponse = await _messageMatcherProcessor.ProcessAsync(analysisResult, message.Text);

                    chat.Response = selectedResponse.Message;
                    chat.ResponseTime = DateTime.UtcNow;
                    chat.SentimentResponse = selectedResponse.Sentiment;

                    // return our reply to the user
                    response = message.CreateReplyMessage(selectedResponse.Message);
                }
                else
                {
                    response = HandleSystemMessage(message);
                }
            }
            catch (Exception ex)
            {
                chat.Error = ex.ToString();
                response = message.CreateReplyMessage("ough!");
            }
            finally
            {
                await _chatRepository.QeueChatAsync(chat);
            }

            return response;
        }

        Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }

            return null;
        }

        QueueChat CreateChatFromMessage(Message message)
        {
            return new QueueChat
            {
                RequestTime = DateTime.UtcNow,
                From = message.From.Name,
                IsRequestFromBot = message.From.IsBot,
                RequestAddress = message.From.Address,
                Request = message.Text,
                To = message.To.Name,
                MessageType = message.Type,
                SourceLanguage = message.SourceLanguage,
                DestinationLanguage = message.Language,
                ConversationId = message.ConversationId,
                ChannelId = message.From.ChannelId,
                HashTags = message.Hashtags
            };
        }
    }
}