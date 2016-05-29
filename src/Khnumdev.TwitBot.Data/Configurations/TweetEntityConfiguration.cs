namespace Khnumdev.TwitBot.Data.Configurations
{
    using Model;
    using System.Data.Entity.ModelConfiguration;

    class TweetEntityConfiguration : EntityTypeConfiguration<Tweet>
    {
        public TweetEntityConfiguration()
        {
            this.Property(i => i.Text)
                .HasMaxLength(150)
                .IsUnicode(true);

            this.Property(i => i.KeyPhrases)
               .HasMaxLength(150)
               .IsUnicode(true);

            this.Property(i => i.Sentiment)
               .IsOptional();

            this.HasRequired(i => i.TwitterUser)
                .WithMany()
                .HasForeignKey(i => i.TwitterUserId);
        }
    }
}
