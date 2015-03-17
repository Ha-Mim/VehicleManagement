namespace VehicleManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vehicle1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        RegNo = c.String(nullable: false),
                        EngineNo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vehicles");
        }
    }
}
