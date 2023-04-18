namespace SIS.REA.Actinium.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationConfiguration",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(maxLength: 4000),
                        Value = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlantNo = c.Long(nullable: false),
                        MaterialNo = c.Long(nullable: false),
                        MaterialDescription = c.String(maxLength: 4000),
                        EanCode = c.String(maxLength: 4000),
                        BatchNo = c.String(maxLength: 4000),
                        PurchaseOrder = c.String(maxLength: 4000),
                        StatusCode = c.String(maxLength: 4000),
                        OrderStartDate = c.String(maxLength: 4000),
                        BestBeforeDate = c.String(maxLength: 4000),
                        NoOfCasesPerPallet = c.Int(nullable: false),
                        CheckDigits = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleActivityPermission",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.ActivityId, t.PermissionId })
                .ForeignKey("dbo.Activity", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Permission", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.ActivityId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Userid = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.Userid, cascadeDelete: true)
                .Index(t => t.Userid)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 4000),
                        FirstName = c.String(maxLength: 4000),
                        LastName = c.String(maxLength: 4000),
                        Password = c.String(maxLength: 4000),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "Userid", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.RoleActivityPermission", "RoleId", "dbo.Role");
            DropForeignKey("dbo.RoleActivityPermission", "PermissionId", "dbo.Permission");
            DropForeignKey("dbo.RoleActivityPermission", "ActivityId", "dbo.Activity");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "Userid" });
            DropIndex("dbo.RoleActivityPermission", new[] { "PermissionId" });
            DropIndex("dbo.RoleActivityPermission", new[] { "ActivityId" });
            DropIndex("dbo.RoleActivityPermission", new[] { "RoleId" });
            DropTable("dbo.User");
            DropTable("dbo.UserRole");
            DropTable("dbo.Role");
            DropTable("dbo.RoleActivityPermission");
            DropTable("dbo.Product");
            DropTable("dbo.Permission");
            DropTable("dbo.ApplicationConfiguration");
            DropTable("dbo.Activity");
        }
    }
}
