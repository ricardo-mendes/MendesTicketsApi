using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsApi.Domain.Exceptions
{
    public class ErrorTransactionException : Exception
    {
        public ErrorTransactionException(string message) : base(message)
        {
            
        }
    }
}
