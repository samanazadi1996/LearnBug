namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Post", "KeyWords", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Post", "KeyWords");
        }
    }
}
