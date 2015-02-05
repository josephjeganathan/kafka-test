using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KafkaNet;
using KafkaNet.Common;
using KafkaNet.Model;
using KafkaNet.Protocol;
using Test.Common;
using Newtonsoft.Json;

namespace TestConsumer.ConsumerGroup.SinglePartition
{
    class Program
    {
        private const int PartitionId = 0;
        private const string Topic = "SinglePartition";
        private const string ConsumerGroup = "ConsumerGroup-SinglePartition-1";
        private static BrokerRoute _brokerRoute;

        static void Main(string[] args)
        {
            try
            {
                var options = new KafkaOptions(new Uri("http://localhost:9092"))
                {
                    Log = new ConsoleLog()
                };
                var brokerRouter = new BrokerRouter(options);
                _brokerRoute = brokerRouter.SelectBrokerRoute(Topic);

                StartConsumer(ConsoleColor.Blue, OffsetFetchRequest().Offset + 1);
                StartConsumer(ConsoleColor.Green, OffsetFetchRequest().Offset + 1);
                StartConsumer(ConsoleColor.Red, OffsetFetchRequest().Offset + 1);
                StartConsumer(ConsoleColor.Yellow, OffsetFetchRequest().Offset + 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static ConsumerMetadataResponse ConsumerMetadataRequest()
        {
            var request = new ConsumerMetadataRequest { ConsumerGroup = ConsumerGroup };

            return _brokerRoute.Connection.SendAsync(request).Result.FirstOrDefault();
        }

        private static OffsetCommitResponse OffsetCommitRequest(int partitionId, long offset, string metadata = null)
        {
            var request = new OffsetCommitRequest
            {
                ConsumerGroup = ConsumerGroup,
                OffsetCommits = new List<OffsetCommit>
                {
                    new OffsetCommit
                    {
                        PartitionId = partitionId,
                        Topic = Topic,
                        Offset = offset,
                        Metadata = metadata
                    }
                }
            };

            return _brokerRoute.Connection.SendAsync(request).Result.FirstOrDefault();
        }

        private static OffsetFetchResponse OffsetFetchRequest()
        {
            var request = new OffsetFetchRequest
            {
                ConsumerGroup = ConsumerGroup,
                Topics = new List<OffsetFetch>
                {
                    new OffsetFetch {PartitionId = PartitionId, Topic = Topic}
                }
            };

            var response = _brokerRoute.Connection.SendAsync(request).Result.FirstOrDefault();
            
            Console.WriteLine(JsonConvert.SerializeObject(response));
            
            return response;
        }

        private static void StartConsumer(ConsoleColor consoleColor, long offSet)
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
                }, new OffsetPosition(PartitionId, offSet));

                foreach (var data in consumer.Consume())
                {
                    Console.ForegroundColor = consoleColor;
                    Console.WriteLine("Response: P{0},O{1} : {2}", data.Meta.PartitionId, data.Meta.Offset, data.Value.ToUtf8String());
                    OffsetCommitRequest(PartitionId, data.Meta.Offset);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            });
        }
    }
}
