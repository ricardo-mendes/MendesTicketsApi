namespace TicketsApi.EfData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _AddGenderInUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Gender", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Gender");
        }
    }
}
