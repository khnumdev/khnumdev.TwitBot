namespace Khnumdev.TwitBot.Data.DWH.Configurations.Dimensions
{
    using Model.Dimensions;
    using System.Data.Entity.ModelConfiguration;

    class MessageSourceEntityConfiguration : EntityTypeConfiguration<MessageSource>
    {
        public MessageSourceEntityConfiguration()
        {
            this.Property(i => i.Source)
                .HasMaxLength(20)
                .IsUnicode();

            this.ToTable("DimMessageSource", "DWH");
        }
    }
}
