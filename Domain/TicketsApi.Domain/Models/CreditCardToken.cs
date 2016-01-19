using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsApi.Domain.Models
{
    public class CreditCardToken
    {
        public Guid CreditCardTokenId { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
