namespace TicketsApi.EfData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _Create_Database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessToken",
                c => new
                    {
                        AccessTokenId = c.Guid(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccessTokenId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Cpf = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        State = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Neighborhood = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccessToken", "UserId", "dbo.User");
            DropIndex("dbo.AccessToken", new[] { "UserId" });
            DropTable("dbo.User");
            DropTable("dbo.AccessToken");
        }
    }
}
