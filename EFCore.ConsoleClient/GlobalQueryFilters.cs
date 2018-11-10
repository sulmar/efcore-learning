using System.Collections.Generic;
using System.Linq;
using EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.ConsoleClient
{

     public class GlobalQueryFilters
    {
        public static void Test()
        {
            GlobalQueryFilterTest();
            DisablingQueryTest();
        }

        private static void GlobalQueryFilterTest()
        {
            System.Console.WriteLine("Global Query Filter Test");

            using(var context = new MyContext())
            {
                var customers = context.Customers.ToList();

                Display(customers);
            }          
        }

        private static void DisablingQueryTest()
        {
            System.Console.WriteLine("Disabling Query Test");

            using(var context = new MyContext())
            {
                var customers = context.Customers.IgnoreQueryFilters().ToList();

                Display(customers);
            }
        }

        private static void Display(IEnumerable<Customer> customers)
        {
            foreach(var customer in customers)
            {
                System.Console.WriteLine($"Customer: {customer.FirstName} {customer.LastName} IsDeleted: {customer.IsDeleted}");
            }
        }
    }
}