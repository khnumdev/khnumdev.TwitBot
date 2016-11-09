namespace Khnumdev.TwitBot.Sync.Startup
{
    using Data;
    using System.Data.Entity;

    public class DatabaseConfiguration : DbConfiguration
    {
        public DatabaseConfiguration()
        {
            SetDatabaseInitializer<TwitBotContext>(null);
        }
    }
}
