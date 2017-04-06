namespace movemate_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Photo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Photo");
        }
    }
}
