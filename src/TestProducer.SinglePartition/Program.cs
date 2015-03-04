using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KafkaNet;
using KafkaNet.Common;
using KafkaNet.Model;
using KafkaNet.Protocol;
using Test.Common;

namespace TestProducer.SinglePartition
{
    /// <summary>
    /// Single Partition topic
    /// bin/kafka-topics.sh --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic SinglePartition
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var options = new KafkaOptions(new Uri("http://localhost:9092"))
                {
                    Log = new ConsoleLog()
                };

                var producer = new Producer(new BrokerRouter(options));

                int i = 0;
                var random = new Random();
                while (true)
                {
                    i++;
                    string message = string.Format("[{0}] {1} - {0}", i, "Message");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    producer.SendMessageAsync("SinglePartition", new[] { new Message(message) });
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(random.Next(0, 10));
                }
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
    }
}
