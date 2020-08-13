namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;
    
    public partial class AddGeoLovation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "GeoLocation", c => c.Geography());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "GeoLocation");
        }
    }
}
