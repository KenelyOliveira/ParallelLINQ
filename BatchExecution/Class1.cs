using System;
using System.Linq;

namespace BatchExecution
{
    public static class Extension
    {
        public static bool IsEven(this int n)
        {
            return n % 2 == 0;
        }
    }

    public class ParallelLINQ
    {
        public void Execute()
        {
            var dataSource = Enumerable.Range(1, 2000000);
            var timestamp = DateTime.Now.Ticks;

            //sequentially
            int even = dataSource.Count(n => n.IsEven());
            Console.WriteLine($"sequentially. ticks: {DateTime.Now.Ticks - timestamp }");

            timestamp = DateTime.Now.Ticks;

            //parallel
            even = dataSource.AsParallel().Count(n => n.IsEven());
            Console.WriteLine($"parallel. ticks: {DateTime.Now.Ticks - timestamp }");

            Console.ReadLine();
        }
    }
}