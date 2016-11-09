namespace Khnumdev.TwitBot.Data.DWH.Initializer
{
    using Seed;
    using System.Data.Entity;

    public class DatabaseInitializer : CreateDatabaseIfNotExists<DWHContext>
    {
        protected override void Seed(DWHContext context)
        {
            SeedGenerator.Generate(context);
        }
    }
}
