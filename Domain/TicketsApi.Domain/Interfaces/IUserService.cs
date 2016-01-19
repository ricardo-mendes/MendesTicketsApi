using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApi.Domain.Models;

namespace TicketsApi.Domain.Interfaces
{
    public interface IUserService
    {
        void AddUser(User user);
        User GetUserByAccessTokenId(Guid accessTokenId);
        User GetUserByCreditCardTokenId(Guid creditCardTokenId);
        User GetUserByEmailAndPassword(string email, string password); 
    }
}
