namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerDocument : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "DocumentPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "DocumentPath");
        }
    }
}
