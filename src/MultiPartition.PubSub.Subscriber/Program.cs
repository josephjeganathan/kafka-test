using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KafkaNet;
using KafkaNet.Common;
using KafkaNet.Model;
using KafkaNet.Protocol;
using Test.Common;

namespace MultiPartition.PubSub.Subscriber
{
    static class Program
    {
        private static BrokerRoute _brokerRoute;
        private static Consumer _consumer;
        private const string Topic = "MultiPartition";
        private const string ConsumerGroup = "ConsumerGroup-MultiPartition-1";

        static void Main()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Topic);
                Console.ForegroundColor = ConsoleColor.White;

                var options = new KafkaOptions(new Uri("http://localhost:9092"))
                {
                    Log = new ConsoleLog()
                };

                var brokerRouter = new BrokerRouter(options);
                _brokerRoute = brokerRouter.SelectBrokerRoute(Topic);
                List<Topic> topics = brokerRouter.GetTopicMetadata(Topic);
                OffsetPosition[] offsetPositions = OffsetFetchRequest(topics.First().Partitions.Select(p => new OffsetFetch
                {
                    Topic = Topic,
                    PartitionId = p.PartitionId
                })).Select(o => new OffsetPosition { Offset = o.Offset + 1, PartitionId = o.PartitionId }).ToArray();
                StartConsumer(offsetPositions);
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

        private static void OffsetCommitRequest(int partitionId, long offset, string metadata = null)
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

            _brokerRoute.Connection.SendAsync(request);
        }

        private static IEnumerable<OffsetFetchResponse> OffsetFetchRequest(IEnumerable<OffsetFetch> offsetFetches)
        {
            var request = new OffsetFetchRequest
            {
                ConsumerGroup = ConsumerGroup,
                Topics = offsetFetches.ToList()
            };

            return _brokerRoute.Connection.SendAsync(request).Result;
        }

        private static void StartConsumer(params OffsetPosition[] offsetPositions)
        {
            var options = new KafkaOptions(new Uri("http://localhost:9092"))
            {
                Log = new ConsoleLog()
            };

            _consumer = new Consumer(new ConsumerOptions(Topic, new BrokerRouter(options))
            {
                Log = new ConsoleLog()
            }, offsetPositions);

            Task.Factory.StartNew(() =>
            {
                foreach (var data in _consumer.Consume())
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(@"Response: P{0},O{1} : {2}", data.Meta.PartitionId, data.Meta.Offset, data.Value.ToUtf8String());
                    Console.ForegroundColor = ConsoleColor.White;
                    OffsetCommitRequest(data.Meta.PartitionId, data.Meta.Offset);
                }
            });
        }
    }
}
