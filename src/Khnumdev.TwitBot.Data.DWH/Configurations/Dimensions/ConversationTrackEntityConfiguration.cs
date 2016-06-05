namespace Khnumdev.TwitBot.Data.DWH.Configurations.Dimensions
{
    using Khnumdev.TwitBot.Data.DWH.Model.Dimensions;
    using System.Data.Entity.ModelConfiguration;

    class ConversationTrackEntityConfiguration : EntityTypeConfiguration<ConversationTrack>
    {
        public ConversationTrackEntityConfiguration()
        {
            this.Property(i => i.ConversationId)
                .HasMaxLength(75)
                .IsUnicode();

            this.ToTable("DimConversation", "DWH");
        }
    }
}
