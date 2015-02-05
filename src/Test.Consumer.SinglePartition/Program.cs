using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KafkaNet;
using KafkaNet.Common;
using KafkaNet.Model;
using Test.Common;

namespace Test.Consumer.SinglePartition
{
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

                //start an out of process thread that runs a consumer that will write all received messages to the console
                Task.Factory.StartNew(() =>
                {
                    var consumer = new KafkaNet.Consumer(new ConsumerOptions("TestHarness", new BrokerRouter(options)) { Log = new ConsoleLog() });
                    foreach (var data in consumer.Consume())
                    {
                        Console.WriteLine("Response: P{0},O{1} : {2}", data.Meta.PartitionId, data.Meta.Offset, data.Value.ToUtf8String());
                    }
                });
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
