﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApi.Domain.Models;

namespace TicketsApi.Domain.Interfaces
{
    public interface ICreditCardTokenService
    {
        CreditCardToken GetCreditCardTokenById(Guid creditCardTokenId);
        CreditCardToken GetCreditCardTokenByUserId(int userId);
        void AddCreditCardToken(CreditCardToken token);        
    }
}
