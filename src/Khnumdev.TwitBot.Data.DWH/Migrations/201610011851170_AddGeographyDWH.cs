namespace Khnumdev.TwitBot.Data.DWH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGeographyDWH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "DWH.DimGeography",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("DWH.FactTrendingTopic", "GeographyId", c => c.Int(nullable: false));
            AddColumn("DWH.DimTrendingTopic", "IsPromoted", c => c.Boolean(nullable: false));
            CreateIndex("DWH.FactTrendingTopic", "GeographyId");
            AddForeignKey("DWH.FactTrendingTopic", "GeographyId", "DWH.DimGeography", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("DWH.FactTrendingTopic", "GeographyId", "DWH.DimGeography");
            DropIndex("DWH.FactTrendingTopic", new[] { "GeographyId" });
            DropColumn("DWH.DimTrendingTopic", "IsPromoted");
            DropColumn("DWH.FactTrendingTopic", "GeographyId");
            DropTable("DWH.DimGeography");
        }
    }
}
