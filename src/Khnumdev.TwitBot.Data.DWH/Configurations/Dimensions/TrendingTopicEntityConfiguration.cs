namespace Khnumdev.TwitBot.Data.DWH.Configurations.Dimensions
{
    using Khnumdev.TwitBot.Data.DWH.Model.Dimensions;
    using System.Data.Entity.ModelConfiguration;


    class TrendingTopicEntityConfiguration : EntityTypeConfiguration<TrendingTopic>
    {
        public TrendingTopicEntityConfiguration()
        {
            this.Property(i => i.Name)
                .HasMaxLength(150)
                .IsUnicode(true);

            this.ToTable("DimTrendingTopic", "DWH");
        }
    }
}
