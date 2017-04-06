namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "PhotoBase", c => c.String());
            DropColumn("dbo.Students", "PhotoUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "PhotoUrl", c => c.String());
            DropColumn("dbo.Students", "PhotoBase");
        }
    }
}
