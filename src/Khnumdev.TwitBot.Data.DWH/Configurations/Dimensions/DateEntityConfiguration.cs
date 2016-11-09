namespace Khnumdev.TwitBot.Data.DWH.Configurations.Dimensions
{
    using Khnumdev.TwitBot.Data.DWH.Model.Dimensions;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    class DateEntityConfiguration : EntityTypeConfiguration<Date>
    {
        public DateEntityConfiguration()
        {
            Property(i => i.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.Year)
                .HasColumnType("char")
               .HasMaxLength(4);

            Property(p => p.QuarterName)
                .HasMaxLength(20);

            Property(p => p.MonthName)
               .HasMaxLength(20);

            Property(p => p.CompleteDate)
                .HasColumnType(Constants.DATE)
                .HasColumnName("Date");

            Property(p => p.Month)
               .HasColumnType(Constants.DATE);

            Property(p => p.Quarter)
               .HasColumnType(Constants.DATE);

            this.ToTable("DimDate", "DWH");
        }
    }
}
