namespace VM.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModelName1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Email", c => c.String());
            AddColumn("dbo.Clients", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Phone");
            DropColumn("dbo.Clients", "Email");
        }
    }
}
