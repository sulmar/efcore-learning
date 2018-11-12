using System;

namespace EFCore.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Generator.Generate();

            DisconnectedEntityGraph.Test();

            TrackingTrackGraph.Test();

            RawSQLQueries.Test();

            GlobalQueryFilters.Test();

            TrackingNoTracking.Test();

            Querying.Test();
        }


        
    }
}
