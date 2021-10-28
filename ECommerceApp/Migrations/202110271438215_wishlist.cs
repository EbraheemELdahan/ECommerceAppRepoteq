namespace ECommerceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wishlist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WishLists",
                c => new
                    {
                        userid = c.String(nullable: false, maxLength: 128),
                        productID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.userid, t.productID })
                .ForeignKey("dbo.AspNetUsers", t => t.userid, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.productID, cascadeDelete: true)
                .Index(t => t.userid)
                .Index(t => t.productID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishLists", "productID", "dbo.Products");
            DropForeignKey("dbo.WishLists", "userid", "dbo.AspNetUsers");
            DropIndex("dbo.WishLists", new[] { "productID" });
            DropIndex("dbo.WishLists", new[] { "userid" });
            DropTable("dbo.WishLists");
        }
    }
}
