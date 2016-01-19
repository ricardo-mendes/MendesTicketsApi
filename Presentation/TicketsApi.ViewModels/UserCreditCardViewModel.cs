using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsApi.ViewModels
{
    public class UserCreditCardViewModel
    {
        public Guid AcessTokenId{ get; set; }
        public bool IsSavingCreditCardInToken { get; set; }

        public long AmountInCents { get; set; }
        public string CreditCardNumber { get; set; }
        public int CreditCardBrand { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string SecurityCode { get; set; }
        public string HolderName { get; set; }
        public int InstallmentCount { get; set; }
    }
}
