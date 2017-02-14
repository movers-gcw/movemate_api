namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "Train", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sections", "Bus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sections", "Metro", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sections", "Tram", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "Tram");
            DropColumn("dbo.Sections", "Metro");
            DropColumn("dbo.Sections", "Bus");
            DropColumn("dbo.Sections", "Train");
        }
    }
}
