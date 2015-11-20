namespace Inventur.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventurItems", "Exported", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventurItems", "Exported");
        }
    }
}
