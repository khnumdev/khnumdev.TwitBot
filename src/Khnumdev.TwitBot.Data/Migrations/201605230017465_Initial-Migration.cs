namespace Khnumdev.TwitBot.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tweets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TweetId = c.Long(nullable: false),
                        Text = c.String(maxLength: 150),
                        TwitterUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TwitterUsers", t => t.TwitterUserId, cascadeDelete: true)
                .Index(t => t.TwitterUserId);
            
            CreateTable(
                "dbo.TwitterUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TwitterId = c.Long(nullable: false),
                        TwitterUsername = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tweets", "TwitterUserId", "dbo.TwitterUsers");
            DropIndex("dbo.Tweets", new[] { "TwitterUserId" });
            DropTable("dbo.TwitterUsers");
            DropTable("dbo.Tweets");
        }
    }
}
