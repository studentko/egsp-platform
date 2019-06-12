namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartureTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DepartureTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayOfWeek = c.Int(nullable: false),
                        DepartureTimes = c.String(nullable: false),
                        BusLineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusLines", t => t.BusLineId, cascadeDelete: true)
                .Index(t => t.BusLineId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DepartureTables", "BusLineId", "dbo.BusLines");
            DropIndex("dbo.DepartureTables", new[] { "BusLineId" });
            DropTable("dbo.DepartureTables");
        }
    }
}
