namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Paths",
                c => new
                    {
                        PathId = c.Int(nullable: false, identity: true),
                        PathName = c.String(),
                        Student_StudentId = c.Int(),
                        Student_StudentId1 = c.Int(),
                        Maker_StudentId = c.Int(),
                    })
                .PrimaryKey(t => t.PathId)
                .ForeignKey("dbo.Students", t => t.Student_StudentId)
                .ForeignKey("dbo.Students", t => t.Student_StudentId1)
                .ForeignKey("dbo.Students", t => t.Maker_StudentId)
                .Index(t => t.Student_StudentId)
                .Index(t => t.Student_StudentId1)
                .Index(t => t.Maker_StudentId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        SectionId = c.Int(nullable: false, identity: true),
                        Vehicle = c.String(),
                        Type = c.String(),
                        Price = c.String(),
                        Description = c.String(),
                        AvailableSeats = c.String(),
                        AvailableHeadgear = c.Boolean(nullable: false),
                        Destination_PointOfInterestId = c.Int(),
                        Path_PathId = c.Int(),
                        Start_PointOfInterestId = c.Int(),
                    })
                .PrimaryKey(t => t.SectionId)
                .ForeignKey("dbo.PointOfInterests", t => t.Destination_PointOfInterestId)
                .ForeignKey("dbo.Paths", t => t.Path_PathId)
                .ForeignKey("dbo.PointOfInterests", t => t.Start_PointOfInterestId)
                .Index(t => t.Destination_PointOfInterestId)
                .Index(t => t.Path_PathId)
                .Index(t => t.Start_PointOfInterestId);
            
            AddColumn("dbo.PointOfInterests", "Section_SectionId", c => c.Int());
            AddColumn("dbo.Students", "Path_PathId", c => c.Int());
            CreateIndex("dbo.Students", "Path_PathId");
            CreateIndex("dbo.PointOfInterests", "Section_SectionId");
            AddForeignKey("dbo.Students", "Path_PathId", "dbo.Paths", "PathId");
            AddForeignKey("dbo.PointOfInterests", "Section_SectionId", "dbo.Sections", "SectionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sections", "Start_PointOfInterestId", "dbo.PointOfInterests");
            DropForeignKey("dbo.Sections", "Path_PathId", "dbo.Paths");
            DropForeignKey("dbo.Sections", "Destination_PointOfInterestId", "dbo.PointOfInterests");
            DropForeignKey("dbo.PointOfInterests", "Section_SectionId", "dbo.Sections");
            DropForeignKey("dbo.Students", "Path_PathId", "dbo.Paths");
            DropForeignKey("dbo.Paths", "Maker_StudentId", "dbo.Students");
            DropForeignKey("dbo.Paths", "Student_StudentId1", "dbo.Students");
            DropForeignKey("dbo.Paths", "Student_StudentId", "dbo.Students");
            DropIndex("dbo.PointOfInterests", new[] { "Section_SectionId" });
            DropIndex("dbo.Sections", new[] { "Start_PointOfInterestId" });
            DropIndex("dbo.Sections", new[] { "Path_PathId" });
            DropIndex("dbo.Sections", new[] { "Destination_PointOfInterestId" });
            DropIndex("dbo.Students", new[] { "Path_PathId" });
            DropIndex("dbo.Paths", new[] { "Maker_StudentId" });
            DropIndex("dbo.Paths", new[] { "Student_StudentId1" });
            DropIndex("dbo.Paths", new[] { "Student_StudentId" });
            DropColumn("dbo.Students", "Path_PathId");
            DropColumn("dbo.PointOfInterests", "Section_SectionId");
            DropTable("dbo.Sections");
            DropTable("dbo.Paths");
        }
    }
}
