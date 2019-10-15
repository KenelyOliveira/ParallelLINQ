using System;
using System.Collections.Generic;
using System.Linq;

namespace PLINQ
{
    public static class Extension
    {
        public static bool IsEven(this int n)
        {
            return n % 2 == 0;
        }
    }

    internal static class Program
    {
        private enum ExecutionModes
        {
            Sequential,
            Parallel
        }

        private static IEnumerable<int> DataSource { get; set; }

        private static void CreateDataSource(int numberOfIterations) => DataSource = Enumerable.Range(1, numberOfIterations);

        private static void Log(string message) => Console.WriteLine(message);

        private static void Main(string[] args)
        {
            Log("Working with small data sourcers, there's a loss of performance since it takes more " +
                "time to create and manage multiple threads than to iterate through the collection.");

            CreateDataSource(200);

            Process(ExecutionModes.Sequential);
            Process(ExecutionModes.Parallel, 1);
            Process(ExecutionModes.Parallel, 2);
            Process(ExecutionModes.Parallel, 3);

            Log("Working with large data sourcers, there's a loss of performance since it takes more " +
               "time to create and manage multiple threads than to iterate through the collection.");

            CreateDataSource(int.MaxValue);

            Process(ExecutionModes.Sequential);
            Process(ExecutionModes.Parallel, 1);
            Process(ExecutionModes.Parallel, 2);
            Process(ExecutionModes.Parallel, 3);

            Console.ReadLine();
        }

        private static void Process(ExecutionModes executionModes, int degreeOfParalellism = 0)
        {
            var timeSpan = DateTime.Now.TimeOfDay;
            int count = 0;

            switch (executionModes)
            {
                case ExecutionModes.Sequential:
                    count = DataSource.Count(n => n.IsEven());
                    break;

                case ExecutionModes.Parallel:
                    count = DataSource.AsParallel().WithDegreeOfParallelism(degreeOfParalellism).Count(n => n.IsEven());
                    break;

                default:
                    break;
            }

            Log($"Processing with {Enum.GetName(typeof(ExecutionModes), executionModes)}, {degreeOfParalellism} Degrees of paralellism. TimeSpan: {DateTime.Now.TimeOfDay - timeSpan}, for {count} interactions");
        }
    }
}