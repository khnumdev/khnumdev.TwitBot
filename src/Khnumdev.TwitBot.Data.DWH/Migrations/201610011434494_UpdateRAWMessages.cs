namespace Khnumdev.TwitBot.Data.DWH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRAWMessages : DbMigration
    {
        public override void Up()
        {
            AddColumn("Staging.RAWMessage", "FromId", c => c.String(maxLength: 50));
            AddColumn("Staging.RAWMessage", "ToId", c => c.String(maxLength: 50));
            AddColumn("Staging.RAWMessage", "TopicName", c => c.String(maxLength: 500));
            AddColumn("DWH.DimUser", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("DWH.DimUser", "UserId");
            DropColumn("Staging.RAWMessage", "TopicName");
            DropColumn("Staging.RAWMessage", "ToId");
            DropColumn("Staging.RAWMessage", "FromId");
        }
    }
}
