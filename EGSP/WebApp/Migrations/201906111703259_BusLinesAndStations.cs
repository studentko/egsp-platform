namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BusLinesAndStations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LineNumber = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.LineNumber, unique: true, name: "IX_Linenumber");
            
            CreateTable(
                "dbo.BusStations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BusStationBusLines",
                c => new
                    {
                        BusStation_Id = c.Int(nullable: false),
                        BusLine_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BusStation_Id, t.BusLine_Id })
                .ForeignKey("dbo.BusStations", t => t.BusStation_Id, cascadeDelete: true)
                .ForeignKey("dbo.BusLines", t => t.BusLine_Id, cascadeDelete: true)
                .Index(t => t.BusStation_Id)
                .Index(t => t.BusLine_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BusStationBusLines", "BusLine_Id", "dbo.BusLines");
            DropForeignKey("dbo.BusStationBusLines", "BusStation_Id", "dbo.BusStations");
            DropIndex("dbo.BusStationBusLines", new[] { "BusLine_Id" });
            DropIndex("dbo.BusStationBusLines", new[] { "BusStation_Id" });
            DropIndex("dbo.BusLines", "IX_Linenumber");
            DropTable("dbo.BusStationBusLines");
            DropTable("dbo.BusStations");
            DropTable("dbo.BusLines");
        }
    }
}
