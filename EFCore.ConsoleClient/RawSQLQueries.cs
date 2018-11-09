using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCore.ConsoleClient
{
    public class RawSQLQueries
    {
        public static void Test()
        {
            QueryTest();

            ParameterizedQueryTest();

            QueryLINQOperatorsTest();
        }

       

        private static void QueryTest()
        {
            Console.WriteLine("Query Test");

            using (var context = new MyContext())
            {
                var customers = context.Customers.FromSql("select * from Customers where FirstName like 'K%' ").ToList();

                Display(customers);
            }
        }

        private static void ParameterizedQueryTest()
        {
            Console.WriteLine("Parameterized Query Test");

            string firstName = "Kris";

            using (var context = new MyContext())
            {
                var customers = context.Customers.FromSql($"select * from Customers where FirstName = '{firstName}' ").ToList();

                Display(customers);
            }
        }

        private static void QueryLINQOperatorsTest()
        {
            Console.WriteLine("Query LINQ Operators Test");

            using (var context = new MyContext())
            {
                var customers = context.Customers.FromSql("select * from Customers where FirstName like 'K%' ")
                    .OrderBy(c=>c.FirstName)
                    .ToList();

                Display(customers);
            }
        }

        private static void Display(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"Customer: First Name {customer.FirstName} {customer.LastName}" );
            }
        }



    }
}
