namespace TicketsApi.EfData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _AddUserInTransactionCreditCardError : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionCreditCardError", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.TransactionCreditCardError", "UserId");
            AddForeignKey("dbo.TransactionCreditCardError", "UserId", "dbo.User", "UserId", cascadeDelete: true);
            DropColumn("dbo.TransactionCreditCardError", "ErrorCode");
            DropColumn("dbo.TransactionCreditCardError", "TransactionOrderReference");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransactionCreditCardError", "TransactionOrderReference", c => c.String(nullable: false));
            AddColumn("dbo.TransactionCreditCardError", "ErrorCode", c => c.Int());
            DropForeignKey("dbo.TransactionCreditCardError", "UserId", "dbo.User");
            DropIndex("dbo.TransactionCreditCardError", new[] { "UserId" });
            DropColumn("dbo.TransactionCreditCardError", "UserId");
        }
    }
}
