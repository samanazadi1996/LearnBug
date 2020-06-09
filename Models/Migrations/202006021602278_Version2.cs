namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Content", newName: "Post");
            RenameColumn(table: "dbo.Bookmark", name: "contentId", newName: "postId");
            RenameColumn(table: "dbo.Comment", name: "contentId", newName: "postId");
            RenameColumn(table: "dbo.Factor", name: "contentId", newName: "postId");
            RenameIndex(table: "dbo.Bookmark", name: "IX_contentId", newName: "IX_postId");
            RenameIndex(table: "dbo.Comment", name: "IX_contentId", newName: "IX_postId");
            RenameIndex(table: "dbo.Factor", name: "IX_contentId", newName: "IX_postId");
            AddColumn("dbo.Bookmark", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Bookmark", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Bookmark", "InsertDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Bookmark", "DeleteDateTime", c => c.DateTime());
            AddColumn("dbo.Bookmark", "Description", c => c.String());
            AddColumn("dbo.Post", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Post", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Post", "InsertDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Post", "DeleteDateTime", c => c.DateTime());
            AddColumn("dbo.Comment", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Comment", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Comment", "InsertDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comment", "DeleteDateTime", c => c.DateTime());
            AddColumn("dbo.Comment", "Description", c => c.String());
            AddColumn("dbo.User", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "InsertDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.User", "DeleteDateTime", c => c.DateTime());
            AddColumn("dbo.User", "Description", c => c.String());
            AddColumn("dbo.Factor", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Factor", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Factor", "InsertDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Factor", "DeleteDateTime", c => c.DateTime());
            AddColumn("dbo.Factor", "Description", c => c.String());
            AddColumn("dbo.Follow", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Follow", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Follow", "InsertDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Follow", "DeleteDateTime", c => c.DateTime());
            AddColumn("dbo.Follow", "Description", c => c.String());
            AddColumn("dbo.Message", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Message", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Message", "InsertDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Message", "DeleteDateTime", c => c.DateTime());
            AddColumn("dbo.Message", "Description", c => c.String());
            AddColumn("dbo.Transaction", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Transaction", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Transaction", "InsertDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Transaction", "DeleteDateTime", c => c.DateTime());
            AddColumn("dbo.Transaction", "Description", c => c.String());
            AddColumn("dbo.Group", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Group", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Group", "InsertDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Group", "DeleteDateTime", c => c.DateTime());
            AddColumn("dbo.Group", "Description", c => c.String());
            AddColumn("dbo.Setting", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Setting", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Setting", "InsertDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Setting", "DeleteDateTime", c => c.DateTime());
            AddColumn("dbo.Setting", "Description", c => c.String());
            AlterColumn("dbo.Post", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Post", "Description", c => c.String(nullable: false));
            DropColumn("dbo.Setting", "Description");
            DropColumn("dbo.Setting", "DeleteDateTime");
            DropColumn("dbo.Setting", "InsertDateTime");
            DropColumn("dbo.Setting", "IsDeleted");
            DropColumn("dbo.Setting", "IsActive");
            DropColumn("dbo.Group", "Description");
            DropColumn("dbo.Group", "DeleteDateTime");
            DropColumn("dbo.Group", "InsertDateTime");
            DropColumn("dbo.Group", "IsDeleted");
            DropColumn("dbo.Group", "IsActive");
            DropColumn("dbo.Transaction", "Description");
            DropColumn("dbo.Transaction", "DeleteDateTime");
            DropColumn("dbo.Transaction", "InsertDateTime");
            DropColumn("dbo.Transaction", "IsDeleted");
            DropColumn("dbo.Transaction", "IsActive");
            DropColumn("dbo.Message", "Description");
            DropColumn("dbo.Message", "DeleteDateTime");
            DropColumn("dbo.Message", "InsertDateTime");
            DropColumn("dbo.Message", "IsDeleted");
            DropColumn("dbo.Message", "IsActive");
            DropColumn("dbo.Follow", "Description");
            DropColumn("dbo.Follow", "DeleteDateTime");
            DropColumn("dbo.Follow", "InsertDateTime");
            DropColumn("dbo.Follow", "IsDeleted");
            DropColumn("dbo.Follow", "IsActive");
            DropColumn("dbo.Factor", "Description");
            DropColumn("dbo.Factor", "DeleteDateTime");
            DropColumn("dbo.Factor", "InsertDateTime");
            DropColumn("dbo.Factor", "IsDeleted");
            DropColumn("dbo.Factor", "IsActive");
            DropColumn("dbo.User", "Description");
            DropColumn("dbo.User", "DeleteDateTime");
            DropColumn("dbo.User", "InsertDateTime");
            DropColumn("dbo.User", "IsDeleted");
            DropColumn("dbo.User", "IsActive");
            DropColumn("dbo.Comment", "Description");
            DropColumn("dbo.Comment", "DeleteDateTime");
            DropColumn("dbo.Comment", "InsertDateTime");
            DropColumn("dbo.Comment", "IsDeleted");
            DropColumn("dbo.Comment", "IsActive");
            DropColumn("dbo.Post", "DeleteDateTime");
            DropColumn("dbo.Post", "InsertDateTime");
            DropColumn("dbo.Post", "IsDeleted");
            DropColumn("dbo.Post", "IsActive");
            DropColumn("dbo.Bookmark", "Description");
            DropColumn("dbo.Bookmark", "DeleteDateTime");
            DropColumn("dbo.Bookmark", "InsertDateTime");
            DropColumn("dbo.Bookmark", "IsDeleted");
            DropColumn("dbo.Bookmark", "IsActive");
            RenameIndex(table: "dbo.Factor", name: "IX_postId", newName: "IX_contentId");
            RenameIndex(table: "dbo.Comment", name: "IX_postId", newName: "IX_contentId");
            RenameIndex(table: "dbo.Bookmark", name: "IX_postId", newName: "IX_contentId");
            RenameColumn(table: "dbo.Factor", name: "postId", newName: "contentId");
            RenameColumn(table: "dbo.Comment", name: "postId", newName: "contentId");
            RenameColumn(table: "dbo.Bookmark", name: "postId", newName: "contentId");
            RenameTable(name: "dbo.Post", newName: "Content");
        }
    }
}
