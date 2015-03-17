namespace VehicleManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vehicle2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ScheduleId = c.Int(nullable: false, identity: true),
                        VehicleId = c.Int(nullable: false),
                        Dates = c.DateTime(nullable: false),
                        ShiftId = c.Int(nullable: false),
                        BookedBy = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ScheduleId)
                .ForeignKey("dbo.Shifts", t => t.ShiftId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId)
                .Index(t => t.ShiftId);
            
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        ShiftId = c.Int(nullable: false, identity: true),
                        ShiftName = c.String(),
                    })
                .PrimaryKey(t => t.ShiftId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Schedules", "ShiftId", "dbo.Shifts");
            DropIndex("dbo.Schedules", new[] { "ShiftId" });
            DropIndex("dbo.Schedules", new[] { "VehicleId" });
            DropTable("dbo.Shifts");
            DropTable("dbo.Schedules");
        }
    }
}
