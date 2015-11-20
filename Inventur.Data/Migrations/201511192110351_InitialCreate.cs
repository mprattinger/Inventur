namespace Inventur.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventurItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        ChangedAt = c.DateTime(nullable: false),
                        EANCode = c.String(),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InventurItems");
        }
    }
}
