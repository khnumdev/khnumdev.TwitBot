namespace Khnumdev.TwitBot.Data.DWH.Configurations.Dimensions
{
    using Model.Dimensions;
    using System.Data.Entity.ModelConfiguration;

    class UserEntityConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityConfiguration()
        {
            this.Property(i => i.Name)
                .HasMaxLength(50)
                .IsUnicode();

            this.ToTable("DimUser", "DWH");
        }
    }
}
