namespace Khnumdev.TwitBot.Data.DWH.Configurations.Facts
{
    using Model.Facts;
    using System.Data.Entity.ModelConfiguration;

    class TopicEntityConfiguration : EntityTypeConfiguration<Topic>
    {
        public TopicEntityConfiguration()
        {
            this.HasRequired(i => i.Date)
                 .WithMany()
                 .HasForeignKey(i => i.DateId);

            this.HasRequired(i => i.TrendingTopic)
                 .WithMany()
                 .HasForeignKey(i => i.TrendingTopicId);

            this.HasRequired(i => i.Geography)
                .WithMany()
                .HasForeignKey(i => i.GeographyId);

            this.ToTable("FactTrendingTopic", "DWH");
        }
    }
}
