using GatewayApiClient.DataContracts.EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApi.Domain.Models;
using TicketsApi.ViewModels;

namespace TicketsApi.Mapper
{
    public class CreditCardMapper
    {
        public UserCreditCard MapToCreditCardInfo(UserCreditCardViewModel model)
        {
            UserCreditCard creditCardInfo = new UserCreditCard
            {
                IsSavingCreditCardInToken = model.IsSavingCreditCardInToken,
                AmountInCents = model.AmountInCents,
                CreditCardNumber = model.CreditCardNumber,
                CreditCardBrand = (CreditCardBrandEnum)model.CreditCardBrand,
                ExpMonth = model.ExpMonth,
                ExpYear = model.ExpYear,
                SecurityCode = model.SecurityCode,
                HolderName = model.HolderName,
                InstallmentCount = model.InstallmentCount
            };

            return creditCardInfo;
        }
    }
}
