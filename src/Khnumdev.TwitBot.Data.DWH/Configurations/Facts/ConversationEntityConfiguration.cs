namespace Khnumdev.TwitBot.Data.DWH.Configurations.Facts
{
    using Model.Facts;
    using System.Data.Entity.ModelConfiguration;

    class ConversationEntityConfiguration : EntityTypeConfiguration<Conversation>
    {
        public ConversationEntityConfiguration()
        {
            this.HasRequired(i => i.Channel)
                 .WithMany()
                 .HasForeignKey(i => i.ChannelId);

            this.HasRequired(i => i.ConversationTrack)
                 .WithMany()
                 .HasForeignKey(i => i.ConversationTrackId);

            this.HasRequired(i => i.Date)
                 .WithMany()
                 .HasForeignKey(i => i.DateId);

            this.HasRequired(i => i.Message)
                 .WithMany()
                 .HasForeignKey(i => i.MessageId);

            this.HasRequired(i => i.MessageSource)
                 .WithMany()
                 .HasForeignKey(i => i.MessageSourceId);

            this.HasRequired(i => i.MessageTye)
                 .WithMany()
                 .HasForeignKey(i => i.MessageTypeId);

            this.HasRequired(i => i.User)
                 .WithMany()
                 .HasForeignKey(i => i.UserId);

            this.ToTable("FactUser", "DWH");
        }
    }
}

