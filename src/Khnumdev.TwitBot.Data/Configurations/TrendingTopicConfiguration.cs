namespace Khnumdev.TwitBot.Data.Configurations
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class TrendingTopicConfiguration : EntityTypeConfiguration<TrendingTopic>
    {
        public TrendingTopicConfiguration()
        {
            this.Property(i => i.Text)
                .HasMaxLength(150)
                .IsUnicode(true);

            this.Property(i => i.Country)
               .HasMaxLength(150)
               .IsUnicode(true);

            this.Property(i => i.Date)
                .HasColumnType("datetime2")
               .IsOptional();
        }
    }
}
