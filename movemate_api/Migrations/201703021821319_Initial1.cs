namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Paths", "DepartmentAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Paths", "DepartmentAddress", c => c.String());
        }
    }
}
