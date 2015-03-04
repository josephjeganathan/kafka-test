using System;
using System.Threading.Tasks;
using KafkaNet;
using KafkaNet.Common;
using KafkaNet.Model;
using Test.Common;

namespace TestConsumer.SinglePartition
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                StartConsumer(ConsoleColor.Blue);
                StartConsumer(ConsoleColor.Green);
                StartConsumer(ConsoleColor.Red);
                StartConsumer(ConsoleColor.Yellow);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                ConsoleLog.WaitOnKeys();
            }
        }

        private static void StartConsumer(ConsoleColor consoleColor)
        {
            var options = new KafkaOptions(new Uri("http://localhost:9092"))
            {
                Log = new ConsoleLog()
            };

            Task.Factory.StartNew(() =>
            {
                var consumer = new Consumer(new ConsumerOptions("SinglePartition", new BrokerRouter(options))
                {
                    Log = new ConsoleLog()
                });
                foreach (var data in consumer.Consume())
                {
                    Console.ForegroundColor = consoleColor;
                    Console.WriteLine("Response: P{0},O{1} : {2}", data.Meta.PartitionId, data.Meta.Offset, data.Value.ToUtf8String());
                    Console.ForegroundColor = ConsoleColor.White;
                }
            });
        }
    }
}
