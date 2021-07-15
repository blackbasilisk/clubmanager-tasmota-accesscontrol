namespace SIS.REA.Actinium.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class processOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ProcessOrder", c => c.String(maxLength: 4000));
            AlterColumn("dbo.Product", "PlantNo", c => c.String(maxLength: 4000));
            AlterColumn("dbo.Product", "MaterialNo", c => c.String(maxLength: 4000));
            DropColumn("dbo.Product", "PurchaseOrder");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "PurchaseOrder", c => c.String(maxLength: 4000));
            AlterColumn("dbo.Product", "MaterialNo", c => c.Long(nullable: false));
            AlterColumn("dbo.Product", "PlantNo", c => c.Long(nullable: false));
            DropColumn("dbo.Product", "ProcessOrder");
        }
    }
}
