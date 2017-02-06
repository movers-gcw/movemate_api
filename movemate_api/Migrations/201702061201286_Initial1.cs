namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.Students", "StudentId", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "FacebookId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Students", new[] { "StudentId", "FacebookId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.Students", "FacebookId", c => c.String());
            AlterColumn("dbo.Students", "StudentId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Students", "StudentId");
        }
    }
}
