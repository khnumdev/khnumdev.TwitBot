namespace Khnumdev.TwitBot.Data.DWH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRAWMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Staging.RAWMessage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequestTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ResponseTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        MessageType = c.String(maxLength: 50),
                        SourceLanguage = c.String(maxLength: 20),
                        DestinationLanguage = c.String(maxLength: 20),
                        IsRequestFromBot = c.Boolean(),
                        RequestAddress = c.String(maxLength: 50),
                        From = c.String(maxLength: 50),
                        To = c.String(maxLength: 50),
                        Request = c.String(maxLength: 500),
                        Response = c.String(maxLength: 500),
                        Error = c.String(),
                        ConversationId = c.String(maxLength: 75),
                        ChannelId = c.String(maxLength: 20),
                        Sentiment = c.Single(nullable: false),
                        SentimentResponse = c.Single(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("Staging.RAWMessage");
        }
    }
}
