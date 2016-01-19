using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsApi.Domain.Exceptions
{
    public class TransactionNotAuthorizedException : Exception 
    {
        public TransactionNotAuthorizedException() : base("Transação não autorizada")
        {
                
        }
    }
}
