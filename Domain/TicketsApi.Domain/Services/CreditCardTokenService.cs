using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApi.Domain.Exceptions;
using TicketsApi.Domain.Interfaces;
using TicketsApi.Domain.Interfaces.Repository;
using TicketsApi.Domain.Models;

namespace TicketsApi.Domain.Services
{
    public class CreditCardTokenService : ICreditCardTokenService
    {
        private ICreditCardTokenRepository _creditCardTokenRepository;

        public CreditCardTokenService(ICreditCardTokenRepository creditCardTokenRepository)
        {
            _creditCardTokenRepository = creditCardTokenRepository;
        }

        public void AddCreditCardToken(CreditCardToken newToken)
        {
            if (UserHasCreditCardToken(newToken.UserId))
            {
                CreditCardToken deletedToken = GetCreditCardTokenByUserId(newToken.UserId);
                _creditCardTokenRepository.DeleteCreditCardToken(deletedToken);
            }
            
            _creditCardTokenRepository.AddCreditCardToken(newToken);
            _creditCardTokenRepository.Save();
        }

        public CreditCardToken GetCreditCardTokenByUserId(int userId)
        {
            var creditCardToken = _creditCardTokenRepository.GetCreditCardTokenByUserId(userId);

            if(creditCardToken == null)
                throw new CreditCardTokenException();

            return creditCardToken;
        }

        public CreditCardToken GetCreditCardTokenById(Guid creditCardTokenId)
        {
            return _creditCardTokenRepository.GetCreditCardTokenById(creditCardTokenId);
        }

        private bool UserHasCreditCardToken(int userId)
        {
            var creditCardToken = _creditCardTokenRepository.GetCreditCardTokenByUserId(userId);

            if (creditCardToken != null)
                return true;

            return false;
        }
    }
}
