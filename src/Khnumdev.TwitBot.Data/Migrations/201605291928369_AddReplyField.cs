namespace Khnumdev.TwitBot.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddReplyField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tweets", "IsReply", c => c.Boolean(nullable: false, defaultValue: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Tweets", "IsReply");
        }
    }
}
