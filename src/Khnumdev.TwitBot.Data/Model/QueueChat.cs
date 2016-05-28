namespace Khnumdev.TwitBot.Data.Model
{
    using System;

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
    }
}
