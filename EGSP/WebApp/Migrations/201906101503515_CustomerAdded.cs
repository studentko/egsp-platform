namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Address = c.String(nullable: false),
                        CustomerTypeId = c.Int(nullable: false),
                        Approoved = c.Boolean(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.CustomerTypes", t => t.CustomerTypeId, cascadeDelete: true)
                .Index(t => t.CustomerTypeId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.CustomerTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(nullable: false),
                        NeedApproval = c.Boolean(nullable: false),
                        NeedPhotoId = c.Boolean(nullable: false),
                        InstructionsToUser = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "CustomerTypeId", "dbo.CustomerTypes");
            DropForeignKey("dbo.Customers", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Customers", new[] { "ApplicationUserId" });
            DropIndex("dbo.Customers", new[] { "CustomerTypeId" });
            DropTable("dbo.CustomerTypes");
            DropTable("dbo.Customers");
        }
    }
}
