namespace Khnumdev.TwitBot.Data.DWH.Configurations.Configuration
{
    using Model.Configuration;
    using System.Data.Entity.ModelConfiguration;

    class StateEntityConfiguration : EntityTypeConfiguration<State>
    {
        public StateEntityConfiguration()
        {
            this.Property(i => i.Name)
                .HasMaxLength(50)
                .IsUnicode();

            this.ToTable("State", "Configuration");
        }
    }
}
