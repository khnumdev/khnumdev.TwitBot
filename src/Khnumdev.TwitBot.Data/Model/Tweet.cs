namespace Khnumdev.TwitBot.Data.Model
{
    public class Tweet
    {
        public int Id { get; set; }

        public long TweetId { get; set; }

        public string Text { get; set; }

        public int TwitterUserId { get; set; }

        public virtual TwitterUser TwitterUser { get; set; }

        public string KeyPhrases { get; set; }

        public float? Sentiment { get; set; }
    }
}
