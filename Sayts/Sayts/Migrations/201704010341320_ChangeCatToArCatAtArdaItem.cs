namespace Sayts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCatToArCatAtArdaItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArdaItems", "ArdaCategory", c => c.Int(nullable: false));
            DropColumn("dbo.ArdaItems", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ArdaItems", "Category", c => c.Int(nullable: false));
            DropColumn("dbo.ArdaItems", "ArdaCategory");
        }
    }
}
