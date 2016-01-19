using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApi.Domain.Interfaces.Repository;
using TicketsApi.Domain.Models;
using TicketsApi.EfData.Context;

namespace TicketsApi.EfData.Repositories
{
    public class UserRepository : IUserRepository
    {
        private TicketsContext _context;

        public UserRepository(TicketsContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public User GetUserByAccessTokenId(Guid accessTokenId)
        {
            var token = _context.AccessTokens.Find(accessTokenId);
            return token.User;
        }

        public User GetUserByCpf(string cpf)
        {
            return _context.Users.Where(u => u.Cpf == cpf).FirstOrDefault();
        }

        public User GetUserByCreditCardTokenId(Guid creditCardTokenId)
        {
            var token = _context.CreditCardsTokens.Find(creditCardTokenId);
            return token.User;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            return _context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
