namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OptimisticLocking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BusLines", "UpdateVersion", c => c.Int(nullable: false));
            AddColumn("dbo.BusStations", "UpdateVersion", c => c.Int(nullable: false));
            AddColumn("dbo.DepartureTables", "UpdateVersion", c => c.Int(nullable: false));
            AddColumn("dbo.PriceEntries", "UpdateVersion", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceEntries", "UpdateVersion");
            DropColumn("dbo.DepartureTables", "UpdateVersion");
            DropColumn("dbo.BusStations", "UpdateVersion");
            DropColumn("dbo.BusLines", "UpdateVersion");
        }
    }
}
