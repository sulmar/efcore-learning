using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFCore.ConsoleClient
{
    public class TrackingNoTracking
    {

        public static void Test()
        {
            TrackingTest();
            NoTrackingTest();
            ContextNoTrackingTest();
        }

        public static void TrackingTest()
        {
              using(var context = new MyContext())
              {
                  var customer = context.Customers.First();

                  customer.IsDeleted = !customer.IsDeleted;

                  context.SaveChanges();
              }
        }

        public static void NoTrackingTest()
        {
              using(var context = new MyContext())
              {
                  var customers = context.Customers.AsNoTracking().ToList();
             }
        }

        public static void ContextNoTrackingTest()
        {

            using (var context = new MyContext())
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                var customers = context.Customers.ToList();
            }
        }

    }
}