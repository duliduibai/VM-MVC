namespace VM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModelName : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountLines",
                c => new
                    {
                        AccountLineId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CostTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AccountLineId)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LastCostTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Age = c.String(),
                        Gender = c.String(),
                        Comment = c.String(),
                        LastLoginTime = c.DateTime(nullable: false),
                        WalletId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            AddColumn("dbo.Orders", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "UserId");
            AddForeignKey("dbo.Orders", "UserId", "dbo.Clients", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.Clients");
            DropForeignKey("dbo.AccountLines", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.AccountLines", new[] { "AccountId" });
            DropColumn("dbo.Orders", "UserId");
            DropTable("dbo.Clients");
            DropTable("dbo.Accounts");
            DropTable("dbo.AccountLines");
        }
    }
}
