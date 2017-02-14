namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sections", "Vehicle", c => c.Int(nullable: false));
            AlterColumn("dbo.Sections", "AvailableSeats", c => c.Int(nullable: false));
            DropColumn("dbo.Sections", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sections", "Type", c => c.String());
            AlterColumn("dbo.Sections", "AvailableSeats", c => c.String());
            AlterColumn("dbo.Sections", "Vehicle", c => c.String());
        }
    }
}
