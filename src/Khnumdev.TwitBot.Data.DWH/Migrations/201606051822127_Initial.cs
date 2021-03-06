namespace Khnumdev.TwitBot.Data.DWH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "DWH.FactConversation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChannelId = c.Int(nullable: false),
                        ConversationTrackId = c.Int(nullable: false),
                        DateId = c.Int(nullable: false),
                        MessageId = c.Int(nullable: false),
                        MessageSourceId = c.Int(nullable: false),
                        MessageTypeId = c.Int(nullable: false),
                        FromUserId = c.Int(nullable: false),
                        ToUserId = c.Int(nullable: false),
                        Sentiment = c.Single(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("DWH.DimChannel", t => t.ChannelId, cascadeDelete: true)
                .ForeignKey("DWH.DimConversation", t => t.ConversationTrackId, cascadeDelete: true)
                .ForeignKey("DWH.DimDate", t => t.DateId, cascadeDelete: true)
                .ForeignKey("DWH.DimUser", t => t.FromUserId)
                .ForeignKey("DWH.DimMessage", t => t.MessageId, cascadeDelete: true)
                .ForeignKey("DWH.DimMessageSource", t => t.MessageSourceId, cascadeDelete: true)
                .ForeignKey("DWH.DimMessageType", t => t.MessageTypeId, cascadeDelete: true)
                .ForeignKey("DWH.DimUser", t => t.ToUserId)
                .Index(t => t.ChannelId)
                .Index(t => t.ConversationTrackId)
                .Index(t => t.DateId)
                .Index(t => t.MessageId)
                .Index(t => t.MessageSourceId)
                .Index(t => t.MessageTypeId)
                .Index(t => t.FromUserId)
                .Index(t => t.ToUserId);
            
            CreateTable(
                "DWH.DimChannel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DWH.DimConversation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConversationId = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DWH.DimDate",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Year = c.String(maxLength: 4, fixedLength: true, unicode: false),
                        Quarter = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        QuarterName = c.String(maxLength: 20),
                        Month = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        MonthName = c.String(maxLength: 20),
                        Day = c.Byte(nullable: false),
                        Hour = c.Byte(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DWH.DimUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DWH.DimMessage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 150),
                        ConversationId = c.String(maxLength: 20),
                        ChannelId = c.String(maxLength: 20),
                        User = c.String(maxLength: 50),
                        Date = c.DateTime(nullable: false),
                        LoadedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DWH.DimMessageSource",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Source = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DWH.DimMessageType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DWH.FactWord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChannelId = c.Int(nullable: false),
                        ConversationTrackId = c.Int(nullable: false),
                        DateId = c.Int(nullable: false),
                        MessageId = c.Int(nullable: false),
                        MessageSourceId = c.Int(nullable: false),
                        MessageTypeId = c.Int(nullable: false),
                        FromUserId = c.Int(nullable: false),
                        ToUserId = c.Int(nullable: false),
                        Content = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("DWH.DimChannel", t => t.ChannelId, cascadeDelete: true)
                .ForeignKey("DWH.DimConversation", t => t.ConversationTrackId, cascadeDelete: true)
                .ForeignKey("DWH.DimDate", t => t.DateId, cascadeDelete: true)
                .ForeignKey("DWH.DimUser", t => t.FromUserId)
                .ForeignKey("DWH.DimMessage", t => t.MessageId, cascadeDelete: true)
                .ForeignKey("DWH.DimMessageSource", t => t.MessageSourceId, cascadeDelete: true)
                .ForeignKey("DWH.DimMessageType", t => t.MessageTypeId, cascadeDelete: true)
                .ForeignKey("DWH.DimUser", t => t.ToUserId)
                .Index(t => t.ChannelId)
                .Index(t => t.ConversationTrackId)
                .Index(t => t.DateId)
                .Index(t => t.MessageId)
                .Index(t => t.MessageSourceId)
                .Index(t => t.MessageTypeId)
                .Index(t => t.FromUserId)
                .Index(t => t.ToUserId);
            
            CreateTable(
                "DWH.DimLanguage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("DWH.FactWord", "ToUserId", "DWH.DimUser");
            DropForeignKey("DWH.FactWord", "MessageTypeId", "DWH.DimMessageType");
            DropForeignKey("DWH.FactWord", "MessageSourceId", "DWH.DimMessageSource");
            DropForeignKey("DWH.FactWord", "MessageId", "DWH.DimMessage");
            DropForeignKey("DWH.FactWord", "FromUserId", "DWH.DimUser");
            DropForeignKey("DWH.FactWord", "DateId", "DWH.DimDate");
            DropForeignKey("DWH.FactWord", "ConversationTrackId", "DWH.DimConversation");
            DropForeignKey("DWH.FactWord", "ChannelId", "DWH.DimChannel");
            DropForeignKey("DWH.FactConversation", "ToUserId", "DWH.DimUser");
            DropForeignKey("DWH.FactConversation", "MessageTypeId", "DWH.DimMessageType");
            DropForeignKey("DWH.FactConversation", "MessageSourceId", "DWH.DimMessageSource");
            DropForeignKey("DWH.FactConversation", "MessageId", "DWH.DimMessage");
            DropForeignKey("DWH.FactConversation", "FromUserId", "DWH.DimUser");
            DropForeignKey("DWH.FactConversation", "DateId", "DWH.DimDate");
            DropForeignKey("DWH.FactConversation", "ConversationTrackId", "DWH.DimConversation");
            DropForeignKey("DWH.FactConversation", "ChannelId", "DWH.DimChannel");
            DropIndex("DWH.FactWord", new[] { "ToUserId" });
            DropIndex("DWH.FactWord", new[] { "FromUserId" });
            DropIndex("DWH.FactWord", new[] { "MessageTypeId" });
            DropIndex("DWH.FactWord", new[] { "MessageSourceId" });
            DropIndex("DWH.FactWord", new[] { "MessageId" });
            DropIndex("DWH.FactWord", new[] { "DateId" });
            DropIndex("DWH.FactWord", new[] { "ConversationTrackId" });
            DropIndex("DWH.FactWord", new[] { "ChannelId" });
            DropIndex("DWH.FactConversation", new[] { "ToUserId" });
            DropIndex("DWH.FactConversation", new[] { "FromUserId" });
            DropIndex("DWH.FactConversation", new[] { "MessageTypeId" });
            DropIndex("DWH.FactConversation", new[] { "MessageSourceId" });
            DropIndex("DWH.FactConversation", new[] { "MessageId" });
            DropIndex("DWH.FactConversation", new[] { "DateId" });
            DropIndex("DWH.FactConversation", new[] { "ConversationTrackId" });
            DropIndex("DWH.FactConversation", new[] { "ChannelId" });
            DropTable("DWH.DimLanguage");
            DropTable("DWH.FactWord");
            DropTable("DWH.DimMessageType");
            DropTable("DWH.DimMessageSource");
            DropTable("DWH.DimMessage");
            DropTable("DWH.DimUser");
            DropTable("DWH.DimDate");
            DropTable("DWH.DimConversation");
            DropTable("DWH.DimChannel");
            DropTable("DWH.FactConversation");
        }
    }
}
