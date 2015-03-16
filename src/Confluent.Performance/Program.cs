using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Client;

namespace Confluent.Performance
{
    class Program
    {
        private const string Topic = "test";
        private static Stopwatch _stopWatch;
        private static long _totalLogs = 0;
        private static ConfluentClient _client;

        static void Main(string[] args)
        {
            _stopWatch = Stopwatch.StartNew();
            var timer = new Timer(PrintStatistics, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            _client = new ConfluentClient(ConfigurationManager.AppSettings["Confluent.BaseUrl"]);


            try
            {
                int batchSize = Convert.ToInt32(ConfigurationManager.AppSettings["Confluent.BatchSize"]);
                var random = new Random();

                if (batchSize <= 0)
                {
                    batchSize = 1;
                }

                while (true)
                {
                    var people = new Person[batchSize];
                    for (int i = 0; i < batchSize; i++)
                    {
                        string name = string.Format("[{0}] {1}", _totalLogs, Guid.NewGuid().ToString("N"));
                        people[i] = new Person { Name = name, Age = random.Next(20, 100) };
                        Interlocked.Increment(ref _totalLogs);
                    }

                    string response = _client.Publish(Topic, people).Result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine("Done!");
                Console.ReadLine();
            }
        }

        private static void PrintStatistics(object state)
        {
            long totalLogs = _totalLogs;

            var perf = new Perf
            {
                HeapMemory = GC.GetTotalMemory(forceFullCollection: false) / 1048576.0,
                LogPerSecond = totalLogs / _stopWatch.Elapsed.TotalSeconds,
                TotalThreads = Process.GetCurrentProcess().Threads.Count
            };

            Console.Clear();
            Console.WriteLine("Total elapsed time: {0}", _stopWatch.Elapsed);
            Console.WriteLine("Total logs: {0}", totalLogs);
            Console.WriteLine("Logs per second: {0}", perf.LogPerSecond);
            Console.WriteLine("Heap size: {0:0.0}MB", perf.HeapMemory);
            Console.WriteLine("Total threads: {0}", perf.TotalThreads);
        }
    }
}
