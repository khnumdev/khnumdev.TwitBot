namespace Khnumdev.TwitBot.Data.DWH.Configurations.Dimensions
{
    using Khnumdev.TwitBot.Data.DWH.Model.Dimensions;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    class DateEntityConfiguration : EntityTypeConfiguration<Date>
    {
        const string DATE = "datetime2";

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
                .HasColumnType(DATE)
                .HasColumnName("Date");

            Property(p => p.Month)
               .HasColumnType(DATE);

            Property(p => p.Quarter)
               .HasColumnType(DATE);

            this.ToTable("DimDate", "DWH");
        }
    }
}
