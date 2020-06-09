namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Post", "Content", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Post", "Content");
        }
    }
}
