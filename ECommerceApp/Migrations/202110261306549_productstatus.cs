namespace ECommerceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "productStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "productStatus");
        }
    }
}
