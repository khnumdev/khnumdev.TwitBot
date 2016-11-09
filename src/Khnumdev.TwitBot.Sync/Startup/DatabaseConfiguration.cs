namespace Khnumdev.TwitBot.Sync.Startup
{
    using Data;
    using Khnumdev.TwitBot.Data.DWH;
    using Khnumdev.TwitBot.Data.DWH.Initializer;
    using System.Data.Entity;

    public class DatabaseConfiguration : DbConfiguration
    {
        public DatabaseConfiguration()
        {
            SetDatabaseInitializer<DWHContext>(new DatabaseInitializer());
            SetDatabaseInitializer<TwitBotContext>(null);
        }
    }
}
