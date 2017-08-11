namespace Noodle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "LotMax");
            DropColumn("dbo.Products", "LotMin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "LotMin", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "LotMax", c => c.Int(nullable: false));
        }
    }
}
