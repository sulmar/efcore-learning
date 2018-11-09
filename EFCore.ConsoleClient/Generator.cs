using EFCore.ConsoleClient.Fakers;
using EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCore.ConsoleClient
{
    public class Generator
    {
        public static void Generate()
        {
            var productFaker = new ProductFaker();
            var serviceFaker = new ServiceFaker();

            var products = productFaker.Generate(100);
            var services = serviceFaker.Generate(40);

            var customerFaker = new CustomerFaker();
            var customers = customerFaker.Generate(50);

            //var items = products.OfType<Item>().Union(services).ToList();

            //var orderFaker = new OrderFaker(customers, items);
            //var orders = orderFaker.Generate(10);


            using (var context = new MyContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Products.AddRange(products);
                context.Services.AddRange(services);
                context.SaveChanges();


                context.Customers.AddRange(customers);
                context.SaveChanges();

                //context.Orders.AddRange(orders);
                //context.SaveChanges();
            }
        }
    }
}
