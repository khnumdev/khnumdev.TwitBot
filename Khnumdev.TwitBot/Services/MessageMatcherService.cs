namespace Khnumdev.TwitBot.Services
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class MessageMatcherService
    {
        static List<string> _messages;

        public bool HasMessages
        {
            get
            {
                return _messages != null;
            }
        }

        public string Match(string input, List<string> twitterMessages)
        {
            if (_messages == null)
            {
                _messages = twitterMessages;
            }

            return _messages
                .Where(i => i.Contains(input))
                .First();
        }
    }
}