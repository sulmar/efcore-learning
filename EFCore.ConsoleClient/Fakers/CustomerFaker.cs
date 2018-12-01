using Bogus;
using EFCore.Models;
using System.Text;

namespace EFCore.ConsoleClient.Fakers
{
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker()
        {
            AddressFaker addressFaker = new AddressFaker();

            StrictMode(true);
            Ignore(p => p.Id);
            RuleFor(u => u.Gender, f => f.PickRandom<Gender>());
            RuleFor(p => p.FirstName, f => f.Name.FirstName());
            RuleFor(p => p.LastName, f => f.Name.LastName());
            RuleFor(p => p.Email, f=>f.Internet.ExampleEmail());
            RuleFor(p => p.ShippingAddress, f => addressFaker.Generate());
            RuleFor(p => p.IsDeleted, f => f.Random.Bool(0.8f));           
            Ignore(p => p.Orders);
            FinishWith((f,c) => System.Console.WriteLine($"Created {c.FirstName} {c.LastName} {c.Email}"));
        }
    }
}
