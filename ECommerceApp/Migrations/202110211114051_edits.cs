namespace ECommerceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edits : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Brands", "Name", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Brands", "Name", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.AspNetUsers", "Discriminator");
        }
    }
}
