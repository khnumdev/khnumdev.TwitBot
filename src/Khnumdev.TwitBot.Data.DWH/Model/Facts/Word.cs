namespace Khnumdev.TwitBot.Data.DWH.Model.Facts
{
    using Dimensions;

    public class Word
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

        public int FromUserId { get; set; }

        public User FromUser { get; set; }

        public int ToUserId { get; set; }

        public User ToUser { get; set; }

        public string Content { get; set; }
    }
}
