namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Paths", "Student_StudentId", "dbo.Students");
            RenameColumn(table: "dbo.Paths", name: "Maker_StudentId", newName: "MakerId");
            RenameIndex(table: "dbo.Paths", name: "IX_Maker_StudentId", newName: "IX_MakerId");
            AddColumn("dbo.Paths", "Student_StudentId1", c => c.Int());
            CreateIndex("dbo.Paths", "Student_StudentId1");
            AddForeignKey("dbo.Paths", "Student_StudentId", "dbo.Students", "StudentId");
            AddForeignKey("dbo.Paths", "Student_StudentId1", "dbo.Students", "StudentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Paths", "Student_StudentId1", "dbo.Students");
            DropForeignKey("dbo.Paths", "Student_StudentId", "dbo.Students");
            DropIndex("dbo.Paths", new[] { "Student_StudentId1" });
            DropColumn("dbo.Paths", "Student_StudentId1");
            RenameIndex(table: "dbo.Paths", name: "IX_MakerId", newName: "IX_Maker_StudentId");
            RenameColumn(table: "dbo.Paths", name: "MakerId", newName: "Maker_StudentId");
            AddForeignKey("dbo.Paths", "Student_StudentId", "dbo.Students", "StudentId");
        }
    }
}
