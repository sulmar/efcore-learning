using Bogus;
using EFCore.Models;
using System.Collections.Generic;
using System.Linq;

namespace EFCore.ConsoleClient.Fakers
{
    public class OrderFaker : Faker<Order>
    {
        public OrderFaker(List<Customer> customers, List<Item> items)
        {
            var orderDetailFaker = new OrderDetailFaker(items);

            StrictMode(true);
            Ignore(p => p.Id);
            RuleFor(p => p.OrderNumber, f => f.Random.Replace("###-##-####"));
            RuleFor(p => p.OrderDate, f => f.Date.Past(3));
            RuleFor(p => p.Customer, f => f.PickRandom(customers));
            RuleFor(p => p.Details, f => orderDetailFaker.Generate(f.Random.Number(1, 5)).ToList());
            Ignore(p => p.DeliveryDate);
            Ignore(p => p.Payment);
            Ignore(p => p.CustomerId);
            FinishWith((f, order) => System.Console.WriteLine($"{order.OrderNumber} {order.Customer.FirstName} {order.Customer.LastName}"));
        }

    }
}
