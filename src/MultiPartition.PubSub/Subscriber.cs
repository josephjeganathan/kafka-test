using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using KafkaNet;
using KafkaNet.Common;
using KafkaNet.Model;
using KafkaNet.Protocol;
using Test.Common;

namespace MultiPartition.PubSub
{
    public partial class Subscriber : Form
    {
        private BrokerRoute _brokerRoute;
        private Consumer _consumer;
        private const string Topic = "MultiPartition";
        private const string ConsumerGroup = "ConsumerGroup-MultiPartition-1";

        public Subscriber()
        {
            InitializeComponent();
        }

        private void Subscriber_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_consumer != null)
            {
                _consumer.Dispose();
            }
        }

        private void OffsetCommitRequest(int partitionId, long offset, string metadata = null)
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

        private IEnumerable<OffsetFetchResponse> OffsetFetchRequest(IEnumerable<OffsetFetch> offsetFetches)
        {
            var request = new OffsetFetchRequest
            {
                ConsumerGroup = ConsumerGroup,
                Topics = offsetFetches.ToList()
            };

            return _brokerRoute.Connection.SendAsync(request).Result;
        }

        private void StartConsumer(params OffsetPosition[] offsetPositions)
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
                    AppendMessage(string.Format("Response: P{0},O{1} : {2}", data.Meta.PartitionId, data.Meta.Offset, data.Value.ToUtf8String()));
                    OffsetCommitRequest(data.Meta.PartitionId, data.Meta.Offset);
                }
            });
        }

        private void AppendMessage(string message)
        {
            if (textBoxMessage.InvokeRequired)
            {
                textBoxMessage.BeginInvoke((MethodInvoker) delegate
                {
                    textBoxMessage.Text += string.Format("\r\n{0}", message);
                });
            }
            else
            {
                textBoxMessage.Text += string.Format("\r\n{0}", message);
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
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
    }
}
