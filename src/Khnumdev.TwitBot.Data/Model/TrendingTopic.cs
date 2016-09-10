namespace Khnumdev.TwitBot.Data.Model
{
    using System;

    public class TrendingTopic
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string Country { get; set; }

        public bool IsPromoted { get; set; }
    }
}
