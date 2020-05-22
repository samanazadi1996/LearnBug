namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookmark",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Datetime = c.DateTime(nullable: false),
                        userId = c.Int(nullable: false),
                        contentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Content", t => t.contentId)
                .ForeignKey("dbo.User", t => t.userId)
                .Index(t => t.userId)
                .Index(t => t.contentId);
            
            CreateTable(
                "dbo.Content",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false),
                        Datetime = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                        Status = c.Byte(),
                        Price = c.Double(),
                        groupId = c.Int(nullable: false),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Group", t => t.groupId)
                .ForeignKey("dbo.User", t => t.userId)
                .Index(t => t.groupId)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Datetime = c.DateTime(nullable: false),
                        userId = c.Int(nullable: false),
                        contentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Content", t => t.contentId)
                .ForeignKey("dbo.User", t => t.userId)
                .Index(t => t.userId)
                .Index(t => t.contentId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        name = c.String(nullable: false),
                        Email = c.String(),
                        Password = c.String(),
                        Dateofbirth = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        Biography = c.String(),
                        Phone = c.String(),
                        Image = c.String(),
                        Location = c.String(),
                        Roles = c.String(),
                        Status = c.Byte(),
                        Wallet = c.Double(nullable: false),
                        Commission = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Factor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Datetime = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        Commission = c.Int(nullable: false),
                        contentId = c.Int(nullable: false),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Content", t => t.contentId)
                .ForeignKey("dbo.User", t => t.userId)
                .Index(t => t.contentId)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Follow",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        followerId = c.Int(nullable: false),
                        followingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.followerId)
                .ForeignKey("dbo.User", t => t.followingId)
                .Index(t => t.followerId)
                .Index(t => t.followingId);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Datetime = c.DateTime(nullable: false),
                        Status = c.Long(),
                        senderId = c.Int(nullable: false),
                        reciverId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.reciverId)
                .ForeignKey("dbo.User", t => t.senderId)
                .Index(t => t.senderId)
                .Index(t => t.reciverId);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Datetime = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        Charge = c.Boolean(nullable: false),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.userId)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        targetId = c.Int(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Setting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookmark", "userId", "dbo.User");
            DropForeignKey("dbo.Bookmark", "contentId", "dbo.Content");
            DropForeignKey("dbo.Content", "userId", "dbo.User");
            DropForeignKey("dbo.Content", "groupId", "dbo.Group");
            DropForeignKey("dbo.Comment", "userId", "dbo.User");
            DropForeignKey("dbo.Transaction", "userId", "dbo.User");
            DropForeignKey("dbo.Message", "senderId", "dbo.User");
            DropForeignKey("dbo.Message", "reciverId", "dbo.User");
            DropForeignKey("dbo.Follow", "followingId", "dbo.User");
            DropForeignKey("dbo.Follow", "followerId", "dbo.User");
            DropForeignKey("dbo.Factor", "userId", "dbo.User");
            DropForeignKey("dbo.Factor", "contentId", "dbo.Content");
            DropForeignKey("dbo.Comment", "contentId", "dbo.Content");
            DropIndex("dbo.Transaction", new[] { "userId" });
            DropIndex("dbo.Message", new[] { "reciverId" });
            DropIndex("dbo.Message", new[] { "senderId" });
            DropIndex("dbo.Follow", new[] { "followingId" });
            DropIndex("dbo.Follow", new[] { "followerId" });
            DropIndex("dbo.Factor", new[] { "userId" });
            DropIndex("dbo.Factor", new[] { "contentId" });
            DropIndex("dbo.Comment", new[] { "contentId" });
            DropIndex("dbo.Comment", new[] { "userId" });
            DropIndex("dbo.Content", new[] { "userId" });
            DropIndex("dbo.Content", new[] { "groupId" });
            DropIndex("dbo.Bookmark", new[] { "contentId" });
            DropIndex("dbo.Bookmark", new[] { "userId" });
            DropTable("dbo.Setting");
            DropTable("dbo.Group");
            DropTable("dbo.Transaction");
            DropTable("dbo.Message");
            DropTable("dbo.Follow");
            DropTable("dbo.Factor");
            DropTable("dbo.User");
            DropTable("dbo.Comment");
            DropTable("dbo.Content");
            DropTable("dbo.Bookmark");
        }
    }
}
