using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsApi.ViewModels
{
    public class UserViewModel
    {
        public string Name { get; set; }

        public string Cpf { get; set; }

        public string BirthDate { get; set; }

        public string Gender { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Neighborhood { get; set; }

        public string Street { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
