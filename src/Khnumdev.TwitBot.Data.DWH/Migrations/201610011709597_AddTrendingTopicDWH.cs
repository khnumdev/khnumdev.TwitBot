namespace Khnumdev.TwitBot.Data.DWH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrendingTopicDWH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "DWH.FactTrendingTopic",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateId = c.Int(nullable: false),
                        TrendingTopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("DWH.DimDate", t => t.DateId, cascadeDelete: true)
                .ForeignKey("DWH.DimTrendingTopic", t => t.TrendingTopicId, cascadeDelete: true)
                .Index(t => t.DateId)
                .Index(t => t.TrendingTopicId);
            
            CreateTable(
                "DWH.DimTrendingTopic",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("DWH.FactTrendingTopic", "TrendingTopicId", "DWH.DimTrendingTopic");
            DropForeignKey("DWH.FactTrendingTopic", "DateId", "DWH.DimDate");
            DropIndex("DWH.FactTrendingTopic", new[] { "TrendingTopicId" });
            DropIndex("DWH.FactTrendingTopic", new[] { "DateId" });
            DropTable("DWH.DimTrendingTopic");
            DropTable("DWH.FactTrendingTopic");
        }
    }
}
