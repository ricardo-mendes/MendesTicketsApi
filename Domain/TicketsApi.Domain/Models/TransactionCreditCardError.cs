using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsApi.Domain.Models
{
    public class TransactionCreditCardError
    {
        public TransactionCreditCardError()
        {
            ErrorDate = DateTime.UtcNow;
        }

        public int TransactionCreditCardErrorId { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }

        [Required]
        public string ErrorDescription { get; set; }

        public DateTime ErrorDate { get; set; }
    }
}
