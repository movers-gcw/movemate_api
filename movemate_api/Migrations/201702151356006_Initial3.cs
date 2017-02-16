namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.PathCarBlobs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PathCarBlobs",
                c => new
                    {
                        PathCarBlobId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        ToFrom = c.Boolean(nullable: false),
                        PathName = c.String(),
                        Date = c.String(),
                        DepId = c.Int(nullable: false),
                        Address = c.String(),
                        Vehicle = c.Int(nullable: false),
                        Seats = c.Int(nullable: false),
                        Price = c.String(),
                    })
                .PrimaryKey(t => t.PathCarBlobId);
            
        }
    }
}
