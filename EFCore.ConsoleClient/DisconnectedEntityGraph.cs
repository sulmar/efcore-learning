using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.ConsoleClient
{
    public class DisconnectedEntityGraph
    {
        public static void AttachTest()
        {
            Console.WriteLine("Attach Test");

            Order order = new Order // root entity
            {
                OrderDate = DateTime.Now,
                OrderNumber = "444-555-666",                

                Customer = new Customer  // child entity
                {
                    Id = 10
                },

                Details = new List<OrderDetail>
                {
                    new OrderDetail { Item = new Product { Id = 5 }, Quantity = 10, UnitPrice = 2.0m },
                    new OrderDetail { Item = new Service { Id = 2 }, Quantity = 5, UnitPrice = 1.5m },
                }
            };

            using (var context = new MyContext())
            {
                context.Orders.Attach(order);
                DisplayStates(context.ChangeTracker.Entries());
            }
        }

        public static void Test()
        {
            AddTest();

            AttachTest();
        }

        public static void AddTest()
        {
            Console.WriteLine("Add Test");

            Order order = new Order // root entity (with key value)
            {
                Id = 1,
                OrderDate = DateTime.Now,
                OrderNumber = "444-555-666",

                Customer = new Customer  // child entity (with key value)
                {
                    Id = 10
                },

                Details = new List<OrderDetail>
                {
                    new OrderDetail { Item = new Product { Id = 5 }, Quantity = 10, UnitPrice = 2.0m },
                    new OrderDetail { Item = new Service { Id = 2 }, Quantity = 5, UnitPrice = 1.5m },
                }
            };

            using (var context = new MyContext())
            {
                context.Orders.Add(order);
                DisplayStates(context.ChangeTracker.Entries());
            }
        }

        private static void DisplayStates(IEnumerable<EntityEntry> entries)
        {
            foreach (EntityEntry entry in entries)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name} State: {entry.State}");
            }
        }
    }
}

