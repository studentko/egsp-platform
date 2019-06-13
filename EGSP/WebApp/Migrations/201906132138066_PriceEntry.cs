namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceEntry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriceEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriceDate = c.DateTime(nullable: false),
                        TicketType = c.Int(nullable: false),
                        CustomerTypeId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerTypes", t => t.CustomerTypeId, cascadeDelete: true)
                .Index(t => t.CustomerTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceEntries", "CustomerTypeId", "dbo.CustomerTypes");
            DropIndex("dbo.PriceEntries", new[] { "CustomerTypeId" });
            DropTable("dbo.PriceEntries");
        }
    }
}
