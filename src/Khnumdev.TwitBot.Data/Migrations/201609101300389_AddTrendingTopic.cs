namespace Khnumdev.TwitBot.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrendingTopic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrendingTopics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(maxLength: 150),
                        Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        Country = c.String(maxLength: 150),
                        IsPromoted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TrendingTopics");
        }
    }
}
