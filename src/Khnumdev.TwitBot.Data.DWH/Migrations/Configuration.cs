namespace Khnumdev.TwitBot.Data.DWH.Migrations
{
    using Model.Dimensions;
    using Seed;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using EntityFramework.BulkInsert.Extensions;

    internal sealed class Configuration : DbMigrationsConfiguration<Khnumdev.TwitBot.Data.DWH.DWHContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Khnumdev.TwitBot.Data.DWH.DWHContext context)
        {
            PopulateStatus(context);
        }

        void PopulateStatus(Khnumdev.TwitBot.Data.DWH.DWHContext context)
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
    }
}
