namespace Khnumdev.TwitBot.Data.DWH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedLengthConversationId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("DWH.DimConversation", "ConversationId", c => c.String(maxLength: 75));
        }
        
        public override void Down()
        {
            AlterColumn("DWH.DimConversation", "ConversationId", c => c.String(maxLength: 20));
        }
    }
}
