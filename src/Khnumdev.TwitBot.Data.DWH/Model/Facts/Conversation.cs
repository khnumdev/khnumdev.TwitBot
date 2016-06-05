namespace Khnumdev.TwitBot.Data.DWH.Model.Facts
{
    using Dimensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Conversation
    {
        public int Id { get; set; }

        public int ChannelId { get; set; }

        public Channel Channel { get; set; }

        public int ConversationTrackId { get; set; }

        public ConversationTrack ConversationTrack { get; set; }

        public int DateId { get; set; }

        public Date Date { get; set; }

        public int MessageId { get; set; }

        public Message Message { get; set; }

        public int MessageSourceId { get; set; }

        public MessageSource MessageSource { get; set; }

        public int MessageTypeId { get; set; }

        public MessageType MessageTye { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public float? Sentiment { get; set; }
    }
}
