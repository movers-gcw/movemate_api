namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Paths", "Maker_StudentId", "dbo.Students");
            DropForeignKey("dbo.PointOfInterests", "Path_PathId", "dbo.Paths");
            DropIndex("dbo.Paths", new[] { "Maker_StudentId" });
            DropIndex("dbo.PointOfInterests", new[] { "Path_PathId" });
            AlterColumn("dbo.Paths", "Maker_StudentId", c => c.Int(nullable: false));
            AlterColumn("dbo.PointOfInterests", "Path_PathId", c => c.Int(nullable: false));
            CreateIndex("dbo.Paths", "Maker_StudentId");
            CreateIndex("dbo.PointOfInterests", "Path_PathId");
            AddForeignKey("dbo.Paths", "Maker_StudentId", "dbo.Students", "StudentId", cascadeDelete: true);
            AddForeignKey("dbo.PointOfInterests", "Path_PathId", "dbo.Paths", "PathId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PointOfInterests", "Path_PathId", "dbo.Paths");
            DropForeignKey("dbo.Paths", "Maker_StudentId", "dbo.Students");
            DropIndex("dbo.PointOfInterests", new[] { "Path_PathId" });
            DropIndex("dbo.Paths", new[] { "Maker_StudentId" });
            AlterColumn("dbo.PointOfInterests", "Path_PathId", c => c.Int());
            AlterColumn("dbo.Paths", "Maker_StudentId", c => c.Int());
            CreateIndex("dbo.PointOfInterests", "Path_PathId");
            CreateIndex("dbo.Paths", "Maker_StudentId");
            AddForeignKey("dbo.PointOfInterests", "Path_PathId", "dbo.Paths", "PathId");
            AddForeignKey("dbo.Paths", "Maker_StudentId", "dbo.Students", "StudentId");
        }
    }
}
