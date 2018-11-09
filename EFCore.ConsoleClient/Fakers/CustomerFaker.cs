using Bogus;
using EFCore.Models;
using System.Text;

namespace EFCore.ConsoleClient.Fakers
{
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker()
        {
            StrictMode(true);
            Ignore(p => p.Id);
            RuleFor(u => u.Gender, f => f.PickRandom<Gender>());
            RuleFor(p => p.FirstName, f => f.Name.FirstName());
            RuleFor(p => p.LastName, f => f.Name.LastName());
            Ignore(p => p.Orders);
        }
    }
}
