namespace Khnumdev.TwitBot.Data.DWH.Seed
{
    using EntityFramework.BulkInsert.Extensions;
    using Model.Configuration;
    using Model.Dimensions;
    using System.Data.Entity.Migrations;
    using System.Linq;

    class SeedGenerator
    {
        public static void Generate(DWHContext context)
        {
            if (!context.Set<Date>().Any())
            {
                PopulateData(context);
                PopulateConfiguration(context);
            }
        }

        static void PopulateData(Khnumdev.TwitBot.Data.DWH.DWHContext context)
        {
            var messageTypes = new MessageSourceSeed().Generate();
            var dates = new DateSeed().Generate();

            context.Set<MessageSource>().AddOrUpdate(messageTypes.ToArray());

            if (!context.Set<Date>().Any())
            {
                context.BulkInsert(dates.ToArray());
            }

            context.SaveChanges();
        }

        static void PopulateConfiguration(Khnumdev.TwitBot.Data.DWH.DWHContext context)
        {
            var isStagingEnabled = new State { Id = 1, IsEnabled = true, Name = "IsStagingEnabled" };
            context.Set<State>().AddOrUpdate(isStagingEnabled);

            context.SaveChanges();
        }
    }
}
