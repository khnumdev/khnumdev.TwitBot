namespace Khnumdev.TwitBot.Data.DWH.Configurations.Dimensions
{
    using Model.Dimensions;
    using System.Data.Entity.ModelConfiguration;

    class ChannelEntityConfiguration : EntityTypeConfiguration<Channel>
    {
        public ChannelEntityConfiguration()
        {
            this.Property(i => i.Name)
                .HasMaxLength(20)
                .IsUnicode();

            this.ToTable("DimChannel", "DWH");
        }
    }
}
