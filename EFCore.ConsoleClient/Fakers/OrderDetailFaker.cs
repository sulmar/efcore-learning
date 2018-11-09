using Bogus;
using EFCore.Models;
using System.Collections.Generic;

namespace EFCore.ConsoleClient.Fakers
{
    public class OrderDetailFaker : Faker<OrderDetail>
    {
        public OrderDetailFaker(List<Item> items)
        {
            StrictMode(true);
            Ignore(p => p.Id);
            RuleFor(p => p.Item, f => f.PickRandom(items));
            RuleFor(p => p.Quantity, f => f.Random.Number(1, 10));
            RuleFor(p => p.UnitPrice, f => f.Random.Decimal(1, 100));
            FinishWith((f, detail) => System.Console.WriteLine($"{detail.Item.Name} {detail.Quantity} {detail.UnitPrice}"));
        }
    }
}
