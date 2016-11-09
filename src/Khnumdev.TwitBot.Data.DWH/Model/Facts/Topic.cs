namespace Khnumdev.TwitBot.Data.DWH.Model.Facts
{
    using Dimensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Topic
    {
        public int Id { get; set; }

        public int DateId { get; set; }

        public Date Date { get; set; }

        public int TrendingTopicId { get; set; }

        public Model.Dimensions.TrendingTopic TrendingTopic { get; set; }

        public int GeographyId { get; set; }

        public Geography Geography { get; set; }
    }
}
