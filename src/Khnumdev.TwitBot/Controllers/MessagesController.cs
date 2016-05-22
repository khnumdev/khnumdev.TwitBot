namespace Khnumdev.TwitBot
{
    using Services;
    using Microsoft.Bot.Connector;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System;
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        readonly TwitterServiceProvider _twitterServiceProvider;
        readonly MessageMatcherService _messageMatcherService;

        public MessagesController()
        {
            _twitterServiceProvider = new TwitterServiceProvider();
            _messageMatcherService = new MessageMatcherService();
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
                List<string> messages = null;

                try
                {
                    if (!_messageMatcherService.HasMessages)
                    {
                        messages = _twitterServiceProvider.GetTweetsFrom(string.Empty);
                    }

                    selectedResponse = _messageMatcherService.Match(message.Text, messages);
                }
                catch (Exception)
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