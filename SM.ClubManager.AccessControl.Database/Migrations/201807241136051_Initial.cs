namespace SIS.REA.Actinium.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CounterLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InScannerPass = c.Int(nullable: false),
                        InScannerFail = c.Int(nullable: false),
                        OutScannerPass = c.Int(nullable: false),
                        OutScannerFail = c.Int(nullable: false),
                        TotalNumberOfPrints = c.Long(nullable: false),
                        NumberOfPrintsSinceClean = c.Long(nullable: false),
                        LastPrintCounterResetDateTime = c.DateTime(nullable: false),
                        LastPassFailCounterResetDateTime = c.DateTime(nullable: false),
                        LastPrintTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CounterLog");
        }
    }
}
