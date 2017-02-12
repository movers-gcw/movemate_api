namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PointOfInterests", "Department_DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Students", "Department_DepartmentId", "dbo.Departments");
            DropIndex("dbo.Students", new[] { "Department_DepartmentId" });
            DropIndex("dbo.Students", new[] { "University_universityId" });
            DropIndex("dbo.Departments", new[] { "University_universityId" });
            DropIndex("dbo.PointOfInterests", new[] { "Department_DepartmentId" });
            DropColumn("dbo.Students", "Name");
            RenameColumn(table: "dbo.Students", name: "Department_DepartmentId", newName: "Name");
            DropPrimaryKey("dbo.Departments");
            DropPrimaryKey("dbo.PointOfInterests");
            AddColumn("dbo.Departments", "Campus_Address", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.PointOfInterests", "Address", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.PointOfInterests", "PoiId", c => c.Int(nullable: false));
            AddColumn("dbo.PointOfInterests", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.PointOfInterests", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Students", "Name", c => c.String(maxLength: 128));
            AlterColumn("dbo.Students", "Name", c => c.String(maxLength: 128));
            AlterColumn("dbo.Departments", "Name", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Departments", "Name");
            AddPrimaryKey("dbo.PointOfInterests", "Address");
            CreateIndex("dbo.Students", "Name");
            CreateIndex("dbo.Students", "University_UniversityId");
            CreateIndex("dbo.Departments", "Campus_Address");
            CreateIndex("dbo.Departments", "University_UniversityId");
            AddForeignKey("dbo.Departments", "Campus_Address", "dbo.PointOfInterests", "Address", cascadeDelete: true);
            AddForeignKey("dbo.Students", "Name", "dbo.Departments", "Name");
            DropColumn("dbo.Departments", "DepartmentId");
            DropColumn("dbo.PointOfInterests", "PointOfInterestId");
            DropColumn("dbo.PointOfInterests", "Name");
            DropColumn("dbo.PointOfInterests", "Department_DepartmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PointOfInterests", "Department_DepartmentId", c => c.Int());
            AddColumn("dbo.PointOfInterests", "Name", c => c.String());
            AddColumn("dbo.PointOfInterests", "PointOfInterestId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Departments", "DepartmentId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Students", "Name", "dbo.Departments");
            DropForeignKey("dbo.Departments", "Campus_Address", "dbo.PointOfInterests");
            DropIndex("dbo.Departments", new[] { "University_UniversityId" });
            DropIndex("dbo.Departments", new[] { "Campus_Address" });
            DropIndex("dbo.Students", new[] { "University_UniversityId" });
            DropIndex("dbo.Students", new[] { "Name" });
            DropPrimaryKey("dbo.PointOfInterests");
            DropPrimaryKey("dbo.Departments");
            AlterColumn("dbo.Departments", "Name", c => c.String());
            AlterColumn("dbo.Students", "Name", c => c.Int());
            AlterColumn("dbo.Students", "Name", c => c.String());
            DropColumn("dbo.PointOfInterests", "Longitude");
            DropColumn("dbo.PointOfInterests", "Latitude");
            DropColumn("dbo.PointOfInterests", "PoiId");
            DropColumn("dbo.PointOfInterests", "Address");
            DropColumn("dbo.Departments", "Campus_Address");
            AddPrimaryKey("dbo.PointOfInterests", "PointOfInterestId");
            AddPrimaryKey("dbo.Departments", "DepartmentId");
            RenameColumn(table: "dbo.Students", name: "Name", newName: "Department_DepartmentId");
            AddColumn("dbo.Students", "Name", c => c.String());
            CreateIndex("dbo.PointOfInterests", "Department_DepartmentId");
            CreateIndex("dbo.Departments", "University_universityId");
            CreateIndex("dbo.Students", "University_universityId");
            CreateIndex("dbo.Students", "Department_DepartmentId");
            AddForeignKey("dbo.Students", "Department_DepartmentId", "dbo.Departments", "DepartmentId");
            AddForeignKey("dbo.PointOfInterests", "Department_DepartmentId", "dbo.Departments", "DepartmentId");
        }
    }
}
