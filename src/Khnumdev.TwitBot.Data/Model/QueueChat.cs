namespace Khnumdev.TwitBot.Data.Model
{
    using System;
    using System.Collections.Generic;

    public class QueueChat
    {
        public DateTime RequestTime { get; set; }

        public DateTime ResponseTime { get; set; }

        public string MessageType { get; set; }

        public string SourceLanguage { get; set; }

        public string DestinationLanguage { get; set; }

        public bool? IsRequestFromBot { get; set; }

        public string RequestAddress { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Request { get; set; }

        public string Response { get; set; }

        public string Error { get; set; }
        public string ConversationId { get; set; }
        public string ChannelId { get; set; }
        public IList<string> HashTags { get; set; }

        public float Sentiment { get; set; }

        public List<string> KeyPhrases { get; set; }
        public float? SentimentResponse { get; set; }
        public string FromId { get; set; }
        public string ToId { get; set; }
        public string TopicName { get; set; }
    }
}
