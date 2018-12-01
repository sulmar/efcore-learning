using Bogus;
using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.ConsoleClient.Fakers
{
    public class AddressFaker : Faker<Address>
    {
        public AddressFaker()
        {
            StrictMode(true);
            RuleFor(p => p.City, f => f.Address.City());
            RuleFor(p => p.Street, f => f.Address.StreetAddress());
            FinishWith((f, c) => Console.WriteLine($"Created {c.City} {c.Street}"));
        }
    }
}
