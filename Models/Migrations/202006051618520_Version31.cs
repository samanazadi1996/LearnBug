namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version31 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bookmark", "Datetime");
            DropColumn("dbo.Post", "Datetime");
            DropColumn("dbo.Comment", "Datetime");
            DropColumn("dbo.Factor", "Datetime");
            DropColumn("dbo.Follow", "DateTime");
            DropColumn("dbo.Message", "Datetime");
            DropColumn("dbo.Transaction", "Datetime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transaction", "Datetime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Message", "Datetime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Follow", "DateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Factor", "Datetime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comment", "Datetime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Post", "Datetime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Bookmark", "Datetime", c => c.DateTime(nullable: false));
        }
    }
}
