namespace Khnumdev.TwitBot.Data.DWH.Configurations.Dimensions
{
    using Model.Dimensions;
    using System.Data.Entity.ModelConfiguration;

    class LanguageEntityConfiguration: EntityTypeConfiguration<Language>
    {
        public LanguageEntityConfiguration()
        {
            this.Property(i => i.Name)
                .HasMaxLength(20)
                .IsUnicode();

            this.ToTable("DimLanguage", "DWH");
        }
    }
}
