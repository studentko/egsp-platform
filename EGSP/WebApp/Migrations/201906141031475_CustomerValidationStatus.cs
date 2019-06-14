namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerValidationStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "ValidationStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "ValidationStatus");
        }
    }
}
