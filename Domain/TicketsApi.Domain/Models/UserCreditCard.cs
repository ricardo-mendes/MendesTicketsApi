using GatewayApiClient.DataContracts.EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsApi.Domain.Models
{
    public class UserCreditCard
    {
        public int UserId { get; set; }
        public bool IsSavingCreditCardInToken { get; set; }

        public long AmountInCents { get; set; }
        public string CreditCardNumber { get; set; }
        public CreditCardBrandEnum CreditCardBrand { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string SecurityCode { get; set; }
        public string HolderName { get; set; }
        public int InstallmentCount { get; set; }
    }
}
