namespace Khnumdev.TwitBot.Data.DWH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDateTime2InMessage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("DWH.DimMessage", "Date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("DWH.DimMessage", "Date", c => c.DateTime(nullable: false));
        }
    }
}
