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
            
            Guid clientId = Guid.NewGuid();
            try
            {
                Uri[] kafkaServerUri = ConfigurationManager.AppSettings["Kafka.Servers"].Split(',').Select(url => new Uri(url)).ToArray();
                int maximumAsyncQueue = Convert.ToInt32(ConfigurationManager.AppSettings["Kafka.MaximumAsyncQueue"]);
                int batchSize = Convert.ToInt32(ConfigurationManager.AppSettings["Kafka.BatchSize"]);
                if (batchSize <= 0)
                {
                    batchSize = 1;
                }
                var options = new KafkaOptions(kafkaServerUri)
                {
                    Log = new NoLog()
                };

                var producer = new Producer(new BrokerRouter(options), maximumAsyncQueue);

                var messages = new Message[batchSize];
                while (true)
                {
                    var messageId = Guid.NewGuid();
                    string message = JsonConvert.SerializeObject(new
                    {
                        Id = messageId,
                        Message = string.Format("[{0}] {1} - {0}", _totalLogs, "Message"),
                        Client = clientId,
                        Time = DateTime.UtcNow
                    });
                    var mod = (int) (_totalLogs % batchSize);
                    messages[mod] = new Message(message, messageId.ToString("N"));
                    _totalLogs++;
                    if (mod == 0)
                    {
                        producer.SendMessageAsync(Topic, messages);
                        messages = new Message[batchSize];
                    }
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
            Console.WriteLine("Total elapsed time: {0}", _stopWatch.Elapsed);
            Console.WriteLine("Total logs: {0}", _totalLogs);
            Console.WriteLine("Logs per second: {0}", perf.LogPerSecond);
            Console.WriteLine("Heap size: {0:0.0}MB", perf.HeapMemory);
            Console.WriteLine("Total threads: {0}", perf.TotalThreads);
        }
    }
}
