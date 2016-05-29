namespace Khnumdev.TwitBot.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoreLengthForKeyPhrases : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tweets", "KeyPhrases", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tweets", "KeyPhrases", c => c.String(maxLength: 100));
        }
    }
}
