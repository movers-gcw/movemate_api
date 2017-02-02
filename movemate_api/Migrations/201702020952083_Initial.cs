namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Email = c.String(),
                        Verified = c.Boolean(),
                        VerificationCode = c.String(),
                        FacebookId = c.String(),
                        GoogleId = c.String(),
                        Department_DepartmentId = c.Int(),
                        University_universityId = c.Int(),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Departments", t => t.Department_DepartmentId)
                .ForeignKey("dbo.Universities", t => t.University_universityId)
                .Index(t => t.Department_DepartmentId)
                .Index(t => t.University_universityId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        University_universityId = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentId)
                .ForeignKey("dbo.Universities", t => t.University_universityId)
                .Index(t => t.University_universityId);
            
            CreateTable(
                "dbo.PointOfInterests",
                c => new
                    {
                        PointOfInterestId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PlaceId = c.String(),
                        Department_DepartmentId = c.Int(),
                    })
                .PrimaryKey(t => t.PointOfInterestId)
                .ForeignKey("dbo.Departments", t => t.Department_DepartmentId)
                .Index(t => t.Department_DepartmentId);
            
            CreateTable(
                "dbo.Universities",
                c => new
                    {
                        universityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.universityId);
            
            
        }
        
        public override void Down()
        {
           
            DropForeignKey("dbo.Students", "University_universityId", "dbo.Universities");
            DropForeignKey("dbo.Departments", "University_universityId", "dbo.Universities");
            DropForeignKey("dbo.Students", "Department_DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.PointOfInterests", "Department_DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
           
            DropIndex("dbo.PointOfInterests", new[] { "Department_DepartmentId" });
            DropIndex("dbo.Departments", new[] { "University_universityId" });
            DropIndex("dbo.Students", new[] { "University_universityId" });
            DropIndex("dbo.Students", new[] { "Department_DepartmentId" });
           
            DropTable("dbo.Universities");
            DropTable("dbo.PointOfInterests");
            DropTable("dbo.Departments");
            DropTable("dbo.Students");
        }
    }
}
