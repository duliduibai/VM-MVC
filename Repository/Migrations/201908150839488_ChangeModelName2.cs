namespace VM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModelName2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "LastLoginTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "LastLoginTime", c => c.DateTime(nullable: false));
        }
    }
}
