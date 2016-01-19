namespace TicketsApi.EfData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _Create_Tables_CreditCardTokens_And_TransactionCreditCardErrors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreditCardToken",
                c => new
                    {
                        CreditCardTokenId = c.Guid(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CreditCardTokenId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TransactionCreditCardError",
                c => new
                    {
                        TransactionCreditCardErrorId = c.Int(nullable: false, identity: true),
                        ErrorCode = c.Int(),
                        ErrorDescription = c.String(nullable: false),
                        TransactionOrderReference = c.String(nullable: false),
                        ErrorDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionCreditCardErrorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CreditCardToken", "UserId", "dbo.User");
            DropIndex("dbo.CreditCardToken", new[] { "UserId" });
            DropTable("dbo.TransactionCreditCardError");
            DropTable("dbo.CreditCardToken");
        }
    }
}
