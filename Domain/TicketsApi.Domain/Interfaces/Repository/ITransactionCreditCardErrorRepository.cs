using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApi.Domain.Models;

namespace TicketsApi.Domain.Interfaces.Repository
{
    public interface ITransactionCreditCardErrorRepository
    {
        void AddTransactionCreditCardError(TransactionCreditCardError transactionCreditCardError);

        void Save();
    }
}
