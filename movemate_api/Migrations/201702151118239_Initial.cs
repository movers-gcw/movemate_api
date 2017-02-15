namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                        Address = c.String(),
                        PlaceId = c.String(),
                        University_UniversityId = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentId)
                .ForeignKey("dbo.Universities", t => t.University_UniversityId)
                .Index(t => t.University_UniversityId);
            
            CreateTable(
                "dbo.Universities",
                c => new
                    {
                        UniversityId = c.Int(nullable: false, identity: true),
                        UniversityName = c.String(),
                    })
                .PrimaryKey(t => t.UniversityId);
            
            CreateTable(
                "dbo.Paths",
                c => new
                    {
                        PathId = c.Int(nullable: false, identity: true),
                        PathName = c.String(),
                        Vehicle = c.Int(nullable: false),
                        Train = c.Boolean(nullable: false),
                        Bus = c.Boolean(nullable: false),
                        Metro = c.Boolean(nullable: false),
                        Tram = c.Boolean(nullable: false),
                        Price = c.String(),
                        Description = c.String(),
                        AvailableSeats = c.Int(nullable: false),
                        AvailableHeadgear = c.Boolean(nullable: false),
                        Destination_PointOfInterestId = c.Int(),
                        Student_StudentId = c.Int(),
                        Maker_StudentId = c.Int(),
                        Start_PointOfInterestId = c.Int(),
                    })
                .PrimaryKey(t => t.PathId)
                .ForeignKey("dbo.PointOfInterests", t => t.Destination_PointOfInterestId)
                .ForeignKey("dbo.Students", t => t.Student_StudentId)
                .ForeignKey("dbo.Students", t => t.Maker_StudentId)
                .ForeignKey("dbo.PointOfInterests", t => t.Start_PointOfInterestId)
                .Index(t => t.Destination_PointOfInterestId)
                .Index(t => t.Student_StudentId)
                .Index(t => t.Maker_StudentId)
                .Index(t => t.Start_PointOfInterestId);
            
            CreateTable(
                "dbo.PointOfInterests",
                c => new
                    {
                        PointOfInterestId = c.Int(nullable: false, identity: true),
                        PlaceId = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Address = c.String(),
                        DateTime = c.String(),
                        Path_PathId = c.Int(),
                    })
                .PrimaryKey(t => t.PointOfInterestId)
                .ForeignKey("dbo.Paths", t => t.Path_PathId)
                .Index(t => t.Path_PathId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Email = c.String(),
                        Verified = c.Boolean(nullable: false),
                        VerificationCode = c.String(),
                        FacebookId = c.String(),
                        GoogleId = c.String(),
                        Department_DepartmentId = c.Int(),
                        University_UniversityId = c.Int(),
                        Path_PathId = c.Int(),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Departments", t => t.Department_DepartmentId)
                .ForeignKey("dbo.Universities", t => t.University_UniversityId)
                .ForeignKey("dbo.Paths", t => t.Path_PathId)
                .Index(t => t.Department_DepartmentId)
                .Index(t => t.University_UniversityId)
                .Index(t => t.Path_PathId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Paths", "Start_PointOfInterestId", "dbo.PointOfInterests");
            DropForeignKey("dbo.Students", "Path_PathId", "dbo.Paths");
            DropForeignKey("dbo.Paths", "Maker_StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "University_UniversityId", "dbo.Universities");
            DropForeignKey("dbo.Paths", "Student_StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "Department_DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Paths", "Destination_PointOfInterestId", "dbo.PointOfInterests");
            DropForeignKey("dbo.PointOfInterests", "Path_PathId", "dbo.Paths");
            DropForeignKey("dbo.Departments", "University_UniversityId", "dbo.Universities");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Students", new[] { "Path_PathId" });
            DropIndex("dbo.Students", new[] { "University_UniversityId" });
            DropIndex("dbo.Students", new[] { "Department_DepartmentId" });
            DropIndex("dbo.PointOfInterests", new[] { "Path_PathId" });
            DropIndex("dbo.Paths", new[] { "Start_PointOfInterestId" });
            DropIndex("dbo.Paths", new[] { "Maker_StudentId" });
            DropIndex("dbo.Paths", new[] { "Student_StudentId" });
            DropIndex("dbo.Paths", new[] { "Destination_PointOfInterestId" });
            DropIndex("dbo.Departments", new[] { "University_UniversityId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Students");
            DropTable("dbo.PointOfInterests");
            DropTable("dbo.Paths");
            DropTable("dbo.Universities");
            DropTable("dbo.Departments");
        }
    }
}
