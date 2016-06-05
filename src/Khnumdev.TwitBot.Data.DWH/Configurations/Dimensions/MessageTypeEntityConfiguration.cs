namespace Khnumdev.TwitBot.Data.DWH.Configurations.Dimensions
{
    using Model.Dimensions;
    using System.Data.Entity.ModelConfiguration;

    class MessageTypeEntityConfiguration : EntityTypeConfiguration<MessageType>
    {
        public MessageTypeEntityConfiguration()
        {
            this.Property(i => i.Name)
                .HasMaxLength(20)
                .IsUnicode();

            this.ToTable("DimMessageType", "DWH");
        }
    }
}
