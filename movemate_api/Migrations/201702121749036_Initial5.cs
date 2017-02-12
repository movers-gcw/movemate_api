namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "Name", "dbo.Departments");
            DropForeignKey("dbo.Departments", "Campus_Address", "dbo.PointOfInterests");
            DropIndex("dbo.Students", new[] { "Name" });
            DropPrimaryKey("dbo.Departments");
            AddColumn("dbo.Students", "Department_DepartmentId", c => c.Int());
            AddColumn("dbo.Departments", "DepartmentId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Students", "Name", c => c.String());
            AlterColumn("dbo.Departments", "Name", c => c.String());
            AddPrimaryKey("dbo.Departments", "DepartmentId");
            CreateIndex("dbo.Students", "Department_DepartmentId");
            AddForeignKey("dbo.Students", "Department_DepartmentId", "dbo.Departments", "DepartmentId");
            AddForeignKey("dbo.Departments", "Campus_Address", "dbo.PointOfInterests", "Address");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "Campus_Address", "dbo.PointOfInterests");
            DropForeignKey("dbo.Students", "Department_DepartmentId", "dbo.Departments");
            DropIndex("dbo.Students", new[] { "Department_DepartmentId" });
            DropPrimaryKey("dbo.Departments");
            AlterColumn("dbo.Departments", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Students", "Name", c => c.String(maxLength: 128));
            DropColumn("dbo.Departments", "DepartmentId");
            DropColumn("dbo.Students", "Department_DepartmentId");
            AddPrimaryKey("dbo.Departments", "Name");
            CreateIndex("dbo.Students", "Name");
            AddForeignKey("dbo.Departments", "Campus_Address", "dbo.PointOfInterests", "Address", cascadeDelete: true);
            AddForeignKey("dbo.Students", "Name", "dbo.Departments", "Name");
        }
    }
}
