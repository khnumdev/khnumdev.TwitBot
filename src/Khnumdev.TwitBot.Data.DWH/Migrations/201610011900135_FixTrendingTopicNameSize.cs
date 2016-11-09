namespace Khnumdev.TwitBot.Data.DWH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTrendingTopicNameSize : DbMigration
    {
        public override void Up()
        {
            AlterColumn("DWH.DimTrendingTopic", "Name", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("DWH.DimTrendingTopic", "Name", c => c.String(maxLength: 20));
        }
    }
}
