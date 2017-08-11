namespace Noodle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bills", "FechaMes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bills", "FechaMes", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
