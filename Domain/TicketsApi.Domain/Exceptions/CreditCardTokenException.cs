using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsApi.Domain.Exceptions
{
    public class CreditCardTokenException : Exception
    {
        public CreditCardTokenException() : base("O senhor(a) nao tem cartão de crédito salvo")
        {

        }
    }
}
