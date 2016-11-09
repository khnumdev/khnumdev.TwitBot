namespace Khnumdev.TwitBot.Data.DWH.Migrations
{
    using Seed;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Khnumdev.TwitBot.Data.DWH.DWHContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Khnumdev.TwitBot.Data.DWH.DWHContext context)
        {
            SeedGenerator.Generate(context);
        }
    }
}
