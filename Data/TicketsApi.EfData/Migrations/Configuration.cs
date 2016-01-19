namespace TicketsApi.EfData.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Domain.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TicketsApi.EfData.Context.TicketsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TicketsApi.EfData.Context.TicketsContext context)
        {

            context.Users.AddOrUpdate(
                  u => u.Name,
                  new User { Name = "Ricardo", Cpf = "134567910" , BirthDate = new DateTime(1995, 6, 29), Gender = "m",
                      State = "RJ", City = "Rio de Janeiro", Neighborhood = "Olaria" , Street = "Rua x",
                      Email = "ricardo@email.com", Password = "123" }
                );

        }
    }
}
