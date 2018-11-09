using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.ConsoleClient
{
    public class TrackingTrackGraph
    {
        public static void Test()
        {
            TrackGraphTest();
        }

        public static void TrackGraphTest()
        {
            Console.WriteLine("TrackGraph Test");

            Order order = new Order //Root entity (with key value)
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
                    new OrderDetail { Item = new Product { Id = 5 }, Quantity = 10, UnitPrice = 2.0m },        // Child entity (empty key)
                    new OrderDetail { Id = 2, Item = new Service { Id = 2 }, Quantity = 5, UnitPrice = 1.5m }, // Child entity (with key value)
                }
            };

            using (var context = new MyContext())
            {
                context.ChangeTracker.TrackGraph(order, e => {

                    if (e.Entry.IsKeySet)
                    {
                        e.Entry.State = EntityState.Unchanged;
                    }
                    else
                    {
                        e.Entry.State = EntityState.Added;
                    }
                });

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
