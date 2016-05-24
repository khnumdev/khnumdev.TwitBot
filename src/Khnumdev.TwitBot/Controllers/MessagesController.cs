namespace Khnumdev.TwitBot
{
    using Microsoft.Bot.Connector;
    using Services;
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;

    [BotAuthentication]
    public class MessagesController : ApiController
    {
        readonly IMessageMatcherProcessor _messageMatcherProcessor;

        public MessagesController(IMessageMatcherProcessor messageMatcherProcesor)
        {
            _messageMatcherProcessor = messageMatcherProcesor;
        }

        [HttpPost]
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            string selectedResponse = default(string);

            if (message.Type == "Message")
            {
                // calculate something for us to return
                int length = (message.Text ?? string.Empty).Length;

                try
                {
                    selectedResponse = await _messageMatcherProcessor.ProcessAsync(message.Text);
                }
                catch (Exception ex)
                {
                    selectedResponse = "agghhhh";
                }

                // return our reply to the user
                return message.CreateReplyMessage(selectedResponse);
            }
            else
            {
                return HandleSystemMessage(message);
            }
        }

        private Message HandleSystemMessage(Message message)
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
    }
}