namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteDescriptionInBaseEntity : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bookmark", "Description");
            DropColumn("dbo.Post", "Description");
            DropColumn("dbo.Comment", "Description");
            DropColumn("dbo.User", "Description");
            DropColumn("dbo.Factor", "Description");
            DropColumn("dbo.Follow", "Description");
            DropColumn("dbo.Message", "Description");
            DropColumn("dbo.Transaction", "Description");
            DropColumn("dbo.Group", "Description");
            DropColumn("dbo.Setting", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Setting", "Description", c => c.String());
            AddColumn("dbo.Group", "Description", c => c.String());
            AddColumn("dbo.Transaction", "Description", c => c.String());
            AddColumn("dbo.Message", "Description", c => c.String());
            AddColumn("dbo.Follow", "Description", c => c.String());
            AddColumn("dbo.Factor", "Description", c => c.String());
            AddColumn("dbo.User", "Description", c => c.String());
            AddColumn("dbo.Comment", "Description", c => c.String());
            AddColumn("dbo.Post", "Description", c => c.String());
            AddColumn("dbo.Bookmark", "Description", c => c.String());
        }
    }
}
