using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApi.Domain.Models;
using TicketsApi.ViewModels;

namespace TicketsApi.Mapper
{
    public class UserMapper
    {
        public User MapToUser(UserViewModel model)
        {
            User user = new User
            {
                Name = model.Name,
                Cpf = model.Cpf,
                BirthDate = Convert.ToDateTime(model.BirthDate),
                Gender = model.Gender,
                State = model.State,
                City = model.City,
                Neighborhood = model.Neighborhood,
                Street = model.Street,
                Email = model.Email,
                Password = model.Password
            };

            return user;
        }
    }
}
