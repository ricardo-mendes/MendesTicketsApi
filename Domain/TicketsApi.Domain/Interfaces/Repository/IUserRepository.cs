using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApi.Domain.Models;

namespace TicketsApi.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUserByAccessTokenId(Guid accessTokenId);
        User GetUserByCpf(string cpf);
        User GetUserByCreditCardTokenId(Guid creditCardTokenId);
        User GetUserByEmail(string email);
        User GetUserByEmailAndPassword(string email, string password);

        void Save();
    }
}
