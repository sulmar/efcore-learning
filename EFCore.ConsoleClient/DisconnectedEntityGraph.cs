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
        public static void Test()
        {
            AttachTest();
            AddTest();
            EntryTest();
            UpdateTest();
            RemoveTest();
        }


        public static void AttachTest()
        {
            Console.WriteLine("Attach Test");

            Order order = new Order // root entity (empty key)
            {
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
                context.Orders.Attach(order);
                DisplayStates(context.ChangeTracker.Entries());
            }
        }

        public static void EntryTest()
        {
            Console.WriteLine("Entry Test");

            Order order = new Order // root entity (empty key)
            {
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
                context.Entry(order).State = EntityState.Modified;

                DisplayStates(context.ChangeTracker.Entries());
            }
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

        private static void UpdateTest()
        {
            Console.WriteLine("Update Test");

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
                    new OrderDetail { Item = new Product { Id = 5 }, Quantity = 10, UnitPrice = 2.0m }, //Child entity (empty key)
                    new OrderDetail { Id = 1, Item = new Service { Id = 2 }, Quantity = 5, UnitPrice = 1.5m }, // //Child entity (with key value)
                }
            };

            using (var context = new MyContext())
            {
                context.Orders.Update(order);
                DisplayStates(context.ChangeTracker.Entries());
            }
        }

        private static void RemoveTest()
        {

            Console.WriteLine("Remove Test");

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
                    new OrderDetail { Item = new Product { Id = 5 }, Quantity = 10, UnitPrice = 2.0m }, //Child entity (empty key)
                    new OrderDetail { Id = 1, Item = new Service { Id = 2 }, Quantity = 5, UnitPrice = 1.5m }, // //Child entity (with key value)
                }
            };

            using (var context = new MyContext())
            {
                context.Orders.Remove(order);
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

