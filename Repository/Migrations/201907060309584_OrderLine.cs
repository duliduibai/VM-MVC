namespace VM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderLine : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderLines", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderLines", "Quantity", c => c.Int(nullable: false));
        }
    }
}
