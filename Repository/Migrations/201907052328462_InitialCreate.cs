namespace VM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        OrderID = c.String(nullable: false, maxLength: 128),
                        ProductID = c.String(maxLength: 128),
                        Quantity = c.Int(nullable: false),
                        Order_OrderID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .Index(t => t.ProductID)
                .Index(t => t.Order_OrderID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        OrderTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Genre = c.String(),
                        Starring = c.String(),
                        SupportingActors = c.String(),
                        Director = c.String(),
                        ScriptWriter = c.String(),
                        ProductionCountry = c.String(),
                        ProductCompany = c.String(),
                        ReleaseYear = c.Int(nullable: false),
                        Language = c.String(),
                        RunTime = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Poster = c.String(),
                        Stock = c.Int(nullable: false),
                        Story = c.String(),
                        StoryAbstract = c.String(),
                    })
                .PrimaryKey(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderLines", "ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderLines", "Order_OrderID", "dbo.Orders");
            DropIndex("dbo.OrderLines", new[] { "Order_OrderID" });
            DropIndex("dbo.OrderLines", new[] { "ProductID" });
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderLines");
        }
    }
}
