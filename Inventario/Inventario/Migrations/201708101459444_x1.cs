namespace Noodle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Configs", newName: "Admins");
            DropForeignKey("dbo.Products", "CategoriesId", "dbo.Categories");
            DropForeignKey("dbo.Investiments", "ProductsId", "dbo.Products");
            DropIndex("dbo.Investiments", new[] { "ProductsId" });
            DropIndex("dbo.Products", new[] { "CategoriesId" });
            DropColumn("dbo.Admins", "Balance");
            DropColumn("dbo.Products", "CategoriesId");
            DropTable("dbo.Categories");
            DropTable("dbo.Investiments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Investiments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductsId = c.Int(nullable: false),
                        Lot = c.Int(nullable: false),
                        Fecha = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "CategoriesId", c => c.Int(nullable: false));
            AddColumn("dbo.Admins", "Balance", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "CategoriesId");
            CreateIndex("dbo.Investiments", "ProductsId");
            AddForeignKey("dbo.Investiments", "ProductsId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "CategoriesId", "dbo.Categories", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.Admins", newName: "Configs");
        }
    }
}
