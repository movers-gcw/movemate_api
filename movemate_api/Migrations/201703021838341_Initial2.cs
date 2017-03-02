namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Paths", "ToFrom", c => c.Boolean(nullable: false));
            AddColumn("dbo.Paths", "DepartmentAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Paths", "DepartmentAddress");
            DropColumn("dbo.Paths", "ToFrom");
        }
    }
}
