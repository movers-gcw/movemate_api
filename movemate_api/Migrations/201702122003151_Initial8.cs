namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "DepartmentName", c => c.String());
            AddColumn("dbo.Universities", "UniversityName", c => c.String());
            DropColumn("dbo.Departments", "Name");
            DropColumn("dbo.Universities", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Universities", "Name", c => c.String());
            AddColumn("dbo.Departments", "Name", c => c.String());
            DropColumn("dbo.Universities", "UniversityName");
            DropColumn("dbo.Departments", "DepartmentName");
        }
    }
}
