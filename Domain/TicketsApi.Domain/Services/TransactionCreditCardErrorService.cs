using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApi.Domain.Interfaces;
using TicketsApi.Domain.Interfaces.Repository;
using TicketsApi.Domain.Models;

namespace TicketsApi.Domain.Services
{
    public class TransactionCreditCardErrorService : ITransactionCreditCardErrorService
    {
        private ITransactionCreditCardErrorRepository _transactionCreditCardErrorRepository;

        public TransactionCreditCardErrorService(ITransactionCreditCardErrorRepository transactionCreditCardErrorRepository)
        {
            _transactionCreditCardErrorRepository = transactionCreditCardErrorRepository;
        }


        public void AddTransactionCreditCardError(TransactionCreditCardError transactionCreditCardError)
        {
            _transactionCreditCardErrorRepository.AddTransactionCreditCardError(transactionCreditCardError);
            _transactionCreditCardErrorRepository.Save();
        }
    }
}
