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
    public class CreditCardTokenRepository : ICreditCardTokenRepository
    {
        private TicketsContext _context;

        public CreditCardTokenRepository(TicketsContext context)
        {
            _context = context;
        }

        public void AddCreditCardToken(CreditCardToken token)
        {
            _context.CreditCardsTokens.Add(token);
        }

        public void DeleteCreditCardToken(CreditCardToken token)
        {
            _context.CreditCardsTokens.Remove(token);
        }

        public CreditCardToken GetCreditCardTokenByUserId(int userId)
        {
            return _context.CreditCardsTokens.Where(t => t.UserId == userId).FirstOrDefault();
        }

        public CreditCardToken GetCreditCardTokenById(Guid creditCardTokenId)
        {
            return _context.CreditCardsTokens.Find(creditCardTokenId);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
