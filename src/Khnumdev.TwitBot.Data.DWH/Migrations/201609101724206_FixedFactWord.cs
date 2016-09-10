namespace Khnumdev.TwitBot.Data.DWH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedFactWord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "DWH.DimWord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("DWH.FactWord", "WordId", c => c.Int(nullable: false));
            CreateIndex("DWH.FactWord", "WordId");
            AddForeignKey("DWH.FactWord", "WordId", "DWH.FactWord", "Id");
            DropColumn("DWH.FactWord", "Content");
        }
        
        public override void Down()
        {
            AddColumn("DWH.FactWord", "Content", c => c.String(maxLength: 20));
            DropForeignKey("DWH.FactWord", "WordId", "DWH.FactWord");
            DropIndex("DWH.FactWord", new[] { "WordId" });
            DropColumn("DWH.FactWord", "WordId");
            DropTable("DWH.DimWord");
        }
    }
}
