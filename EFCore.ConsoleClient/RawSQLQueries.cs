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
            ExecuteSqlTest();

            QueryTest();

            ParameterizedQueryTest();

            QueryLINQOperatorsTest();

            ExecuteReaderTest();
        }

        private static void ExecuteSqlTest()
        {
            System.Console.WriteLine("Execute SQL Test");

            using(var context = new MyContext())
            {
                var commandText = "UPDATE Customers SET IsDeleted = 1 WHERE Id = 1";
                context.Database.ExecuteSqlCommand(commandText);
            }
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


        // private static void NamedParameterizedQueryTest()
        // {
        //     Console.WriteLine("Named Parameterized Query Test");

        //     string firstName = "Kris";

        //     var firstNameParameter = new SqlParameter("firstname", firstName);

        //     using (var context = new MyContext())
        //     {
        //         var customers = context.Customers.FromSql($"select * from Customers where FirstName = @firstname", firstNameParameter).ToList();

        //         Display(customers);
        //     }
        // }

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

        private static void ExecuteReaderTest()
        {
            using (var context = new MyContext())
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * From Customers";
                context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    System.Console.WriteLine($"{result.GetString(2)}, {result.GetBoolean(4)}");
                    // do something with result
                }
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
