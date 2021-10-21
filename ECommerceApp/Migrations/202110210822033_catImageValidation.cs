namespace ECommerceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class catImageValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "CatName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "CatName", c => c.String());
        }
    }
}
