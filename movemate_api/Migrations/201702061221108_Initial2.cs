namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.Students", "StudentId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Students", "FacebookId", c => c.String());
            AddPrimaryKey("dbo.Students", "StudentId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.Students", "FacebookId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Students", "StudentId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Students", new[] { "StudentId", "FacebookId" });
        }
    }
}
