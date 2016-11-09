namespace Khnumdev.TwitBot.Data.DWH.Model.Dimensions
{
    public class TrendingTopic : IDimension
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsPromoted { get; set; }
    }
}
