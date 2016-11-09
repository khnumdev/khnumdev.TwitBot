namespace Khnumdev.TwitBot.Data.DWH.Configurations.Dimensions
{
    using Khnumdev.TwitBot.Data.DWH.Model.Dimensions;
    using System.Data.Entity.ModelConfiguration;

    class GeographyEntityConfiguration : EntityTypeConfiguration<Geography>
    {
        public GeographyEntityConfiguration()
        {
            this.Property(i => i.Name)
                .HasMaxLength(50)
                .IsUnicode();

            this.ToTable("DimGeography", "DWH");
        }
    }
}
