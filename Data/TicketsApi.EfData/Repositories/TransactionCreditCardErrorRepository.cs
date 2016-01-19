using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApi.Domain.Interfaces.Repository;
using TicketsApi.Domain.Models;
using TicketsApi.EfData.Context;

namespace TicketsApi.EfData.Repositories
{
    public class TransactionCreditCardErrorRepository : ITransactionCreditCardErrorRepository
    {
        private TicketsContext _context;

        public TransactionCreditCardErrorRepository(TicketsContext context)
        {
            _context = context;
        }

        public void AddTransactionCreditCardError(TransactionCreditCardError transactionCreditCardError)
        {
            _context.TransactionsCreditCardErrors.Add(transactionCreditCardError);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
