namespace Khnumdev.TwitBot.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAnalysisMetricsForTweets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tweets", "KeyPhrases", c => c.String(maxLength: 100));
            AddColumn("dbo.Tweets", "Sentiment", c => c.Single());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tweets", "Sentiment");
            DropColumn("dbo.Tweets", "KeyPhrases");
        }
    }
}
