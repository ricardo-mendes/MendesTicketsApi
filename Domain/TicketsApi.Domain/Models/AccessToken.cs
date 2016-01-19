using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsApi.Domain.Models
{
    public class AccessToken
    {
        public Guid AccessTokenId { get; set; }
        public virtual int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
