namespace Khnumdev.TwitBot.Data.DWH.Configurations.Staging
{
    using Model.Staging;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class RAWMessageConfiguration : EntityTypeConfiguration<RAWMessage>
    {
        public RAWMessageConfiguration()
        {
            Property(p => p.RequestTime)
               .HasColumnType(Constants.DATE);

            Property(p => p.ResponseTime)
               .HasColumnType(Constants.DATE);

            Property(i => i.MessageType)
                .HasMaxLength(50)
                .IsUnicode();

            Property(i => i.SourceLanguage)
                .HasMaxLength(20)
                .IsUnicode();

            Property(i => i.DestinationLanguage)
                .HasMaxLength(20)
                .IsUnicode();

            Property(i => i.RequestAddress)
               .HasMaxLength(50)
               .IsUnicode();

            Property(i => i.From)
               .HasMaxLength(50)
               .IsUnicode();

            Property(i => i.To)
               .HasMaxLength(50)
               .IsUnicode();

            Property(i => i.FromId)
              .HasMaxLength(50)
              .IsUnicode();

            Property(i => i.ToId)
               .HasMaxLength(50)
               .IsUnicode();

            Property(i => i.TopicName)
               .HasMaxLength(500)
               .IsUnicode();

            Property(i => i.Request)
               .HasMaxLength(500)
               .IsUnicode();

            Property(i => i.Response)
               .HasMaxLength(500)
               .IsUnicode();

            Property(i => i.Error)
              .IsUnicode();

            Property(i => i.ConversationId)
               .HasMaxLength(75)
               .IsUnicode();

            Property(i => i.ChannelId)
                .HasMaxLength(20)
                .IsUnicode();

            ToTable("RAWMessage", "Staging");
        }
    }
}
