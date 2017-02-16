namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Paths", "Student_StudentId1", "dbo.Students");
            DropForeignKey("dbo.Students", "Path_PathId", "dbo.Paths");
            DropForeignKey("dbo.Paths", "Student_StudentId", "dbo.Students");
            DropIndex("dbo.Paths", new[] { "MakerId" });
            DropIndex("dbo.Paths", new[] { "Student_StudentId" });
            DropIndex("dbo.Paths", new[] { "Student_StudentId1" });
            DropIndex("dbo.Students", new[] { "Path_PathId" });
            DropColumn("dbo.Paths", "Student_StudentId");
            //RenameColumn(table: "dbo.Paths", name: "Student_StudentId", newName: "MakerId");
            CreateTable(
                "dbo.StudentJoinedPaths",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        PathId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.PathId })
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: false)
                .ForeignKey("dbo.Paths", t => t.PathId, cascadeDelete: false)
                .Index(t => t.StudentId)
                .Index(t => t.PathId);
            
            AlterColumn("dbo.Paths", "MakerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Paths", "MakerId");
            AddForeignKey("dbo.Paths", "MakerId", "dbo.Students", "StudentId", cascadeDelete: false);
            DropColumn("dbo.Paths", "Student_StudentId1");
            DropColumn("dbo.Students", "Path_PathId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Path_PathId", c => c.Int());
            AddColumn("dbo.Paths", "Student_StudentId1", c => c.Int());
            DropForeignKey("dbo.Paths", "MakerId", "dbo.Students");
            DropForeignKey("dbo.StudentJoinedPaths", "PathId", "dbo.Paths");
            DropForeignKey("dbo.StudentJoinedPaths", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentJoinedPaths", new[] { "PathId" });
            DropIndex("dbo.StudentJoinedPaths", new[] { "StudentId" });
            DropIndex("dbo.Paths", new[] { "MakerId" });
            AlterColumn("dbo.Paths", "MakerId", c => c.Int());
            DropTable("dbo.StudentJoinedPaths");
            //RenameColumn(table: "dbo.Paths", name: "MakerId", newName: "Student_StudentId");
            AddColumn("dbo.Paths", "Student_StudentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "Path_PathId");
            CreateIndex("dbo.Paths", "Student_StudentId1");
            CreateIndex("dbo.Paths", "Student_StudentId");
            CreateIndex("dbo.Paths", "MakerId");
            AddForeignKey("dbo.Paths", "Student_StudentId", "dbo.Students", "StudentId");
            AddForeignKey("dbo.Students", "Path_PathId", "dbo.Paths", "PathId");
            AddForeignKey("dbo.Paths", "Student_StudentId1", "dbo.Students", "StudentId");
        }
    }
}
