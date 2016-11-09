namespace Khnumdev.TwitBot.DWHIngestionJob.Startup
{
    using Data.DWH;
    using System.Data.Entity;

    public class DatabaseConfiguration : DbConfiguration
    {
        public DatabaseConfiguration()
        {
            SetDatabaseInitializer<DWHContext>(null);
        }
    }
}
