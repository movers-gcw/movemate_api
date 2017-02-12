namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial7 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Departments", name: "Campus_Address", newName: "PointOfInterest_Address");
            RenameIndex(table: "dbo.Departments", name: "IX_Campus_Address", newName: "IX_PointOfInterest_Address");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Departments", name: "IX_PointOfInterest_Address", newName: "IX_Campus_Address");
            RenameColumn(table: "dbo.Departments", name: "PointOfInterest_Address", newName: "Campus_Address");
        }
    }
}
