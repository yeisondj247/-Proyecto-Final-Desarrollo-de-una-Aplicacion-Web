namespace Noodle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "PriceSell");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "PriceSell", c => c.Int(nullable: false));
        }
    }
}
