namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseTime = c.DateTime(nullable: false),
                        CheckinTime = c.DateTime(),
                        TicketType = c.Int(nullable: false),
                        CustomerId = c.Int(),
                        AnonymousCustomerId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Tickets", new[] { "CustomerId" });
            DropTable("dbo.Tickets");
        }
    }
}
