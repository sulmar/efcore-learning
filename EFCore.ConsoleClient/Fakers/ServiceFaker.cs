using Bogus;
using EFCore.Models;
using System;

namespace EFCore.ConsoleClient.Fakers
{
    public class ServiceFaker : Faker<Service>
    {
        public ServiceFaker()
        {
            StrictMode(true);
            Ignore(p => p.Id);
            RuleFor(p => p.Name, f => f.Commerce.Product());
            RuleFor(p => p.Duration, f => f.Date.Timespan(TimeSpan.FromHours(3)));
        }
    }
}
