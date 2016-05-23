namespace Khnumdev.TwitBot.Data.Configurations
{
    using Model;
    using System.Data.Entity.ModelConfiguration;

    class TwitterUserEntityConfiguration: EntityTypeConfiguration<TwitterUser>
    {
        public TwitterUserEntityConfiguration()
        { }
    }
}
