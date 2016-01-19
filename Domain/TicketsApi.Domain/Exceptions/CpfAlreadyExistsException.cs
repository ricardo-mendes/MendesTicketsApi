using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsApi.Domain.Exceptions
{
    public class CpfAlreadyExistsException : Exception
    {
        public CpfAlreadyExistsException() : base("Cpf já existe")
        {

        }
    }
}
