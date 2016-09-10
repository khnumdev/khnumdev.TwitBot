namespace Khnumdev.TwitBot.Data.DWH.Configurations.Dimensions
{
    using Model.Dimensions;
    using System.Data.Entity.ModelConfiguration;

    class SingleWordEntityConfiguration : EntityTypeConfiguration<SingleWord>
    {
        public SingleWordEntityConfiguration()
        {
            this.Property(i => i.Text)
                .HasMaxLength(150)
                .IsUnicode();

            this.ToTable("DimWord", "DWH");
        }
    }
}
