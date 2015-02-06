using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using Newtonsoft.Json;

namespace TestLog.Loader
{
    class Program
    {
        private const string Topic = "LogPerformance";
        private static Stopwatch _stopWatch;
        private static long _totalLogs = 0;

        static void Main(string[] args)
        {
            _stopWatch = Stopwatch.StartNew();
            var timer = new Timer(PrintStatistics, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            var random = new Random();

            Guid clientId = Guid.NewGuid();
            try
            {
                Uri[] kafkaServerUri = ConfigurationManager.AppSettings["Kafka.Servers"].Split(',').Select(url => new Uri(url)).ToArray();
                var options = new KafkaOptions(kafkaServerUri)
                {
                    Log = new NoLog()
                };

                var producer = new Producer(new BrokerRouter(options));

                int i = 0;
                while (true)
                {
                    i++;
                    var messageId = Guid.NewGuid();
                    string message = JsonConvert.SerializeObject(new
                    {
                        Id = messageId,
                        Message = string.Format("[{0}] {1} - {0}", i, "Message"),
                        Client = clientId,
                        Time = DateTime.UtcNow
                    });
                    Interlocked.Increment(ref _totalLogs);
                    producer.SendMessageAsync(Topic, new[] { new Message(message, messageId.ToString("N")) });
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
            var perf = new Perf
            {
                HeapMemory = GC.GetTotalMemory(forceFullCollection: false) / 1048576.0,
                LogPerSecond = _totalLogs / _stopWatch.Elapsed.TotalSeconds,
                TotalThreads = Process.GetCurrentProcess().Threads.Count
            };

            Console.Clear();
            Console.WriteLine("Logs per second: {0}", perf.LogPerSecond);
            Console.WriteLine("Heap size: {0:0.0}MB", perf.HeapMemory);
            Console.WriteLine("Total threads: {0}", perf.TotalThreads);
        }
    }
}
