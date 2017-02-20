namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Photourl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "PhotoUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "PhotoUrl");
        }
    }
}
