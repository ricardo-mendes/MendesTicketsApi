using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsApi.Domain.Exceptions
{
    public class UserRegistrationException : Exception
    {
        public UserRegistrationException() : base ("Erro ao cadastrar um usuário")
        {

        }
    }
}
