namespace Khnumdev.TwitBot.Data.DWH.Migrations
{
    using Model.Dimensions;
    using Seed;
    using System.Data.Entity.Migrations;
    using System.Linq;

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
            var messageTypes = new MessageTypeSeed().Generate();
            var dates = new DateSeed().Generate();
            context.Set<MessageType>().AddOrUpdate(messageTypes.ToArray());
            context.Set<Date>().AddOrUpdate(dates.ToArray());

            context.SaveChanges();
        }
    }
}
