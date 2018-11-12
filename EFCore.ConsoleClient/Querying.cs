using System.Linq;

namespace EFCore.ConsoleClient
{
    public class Querying
    {
        public static void Test()
        {
            GroupByTest();
        }

        private static void GroupByTest()
        {
            using(var context = new MyContext())
            {
              var customersQuery = context.Customers
                .GroupBy(c => c.IsDeleted)
                .Select(c => new { IsDeleted = c.Key, Qty = c.Count() });

                var customersByCity = customersQuery.ToList();
            }
        }
    }   
}