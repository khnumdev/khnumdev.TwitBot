namespace Khnumdev.TwitBot.Data.DWH.Configurations.Dimensions
{
    using Model.Dimensions;
    using System.Data.Entity.ModelConfiguration;

    class MessageEntityConfiguration : EntityTypeConfiguration<Message>
    {
        public MessageEntityConfiguration()
        {
            this.Property(i => i.ChannelId)
                .HasMaxLength(20)
                .IsUnicode();

            this.Property(i => i.Content)
                .HasMaxLength(150)
                .IsUnicode();

            this.Property(i => i.ConversationId)
                .HasMaxLength(75)
                .IsUnicode();

            this.Property(i => i.User)
                .HasMaxLength(50)
                .IsUnicode();

            this.ToTable("DimMessage", "DWH");
        }
    }
}
