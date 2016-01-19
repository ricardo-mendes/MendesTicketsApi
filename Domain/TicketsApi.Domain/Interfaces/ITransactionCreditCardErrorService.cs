using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApi.Domain.Models;

namespace TicketsApi.Domain.Interfaces
{
    public interface ITransactionCreditCardErrorService
    {
        void AddTransactionCreditCardError(TransactionCreditCardError transactionCreditCardError);
    }
}
