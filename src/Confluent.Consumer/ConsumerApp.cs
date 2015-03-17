using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Confluent.Client;
using Newtonsoft.Json;

namespace Confluent.Consumer
{
    public partial class ConsumerApp : Form
    {
        private readonly ConfluentClient _client;

        public ConsumerApp()
        {
            InitializeComponent();
            _client = new ConfluentClient(ConfigurationManager.AppSettings["Confluent.BaseUrl"]);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonOffSet.Enabled = false;

            Task.Run(() =>
            {
                //string deleteResponse = _client.DeleteConsumer(textBoxConsumerGroup.Text, textBoxConsumerId.Text).Result;
                //PrependMessage(deleteResponse);
                string createResponse = _client.CreateConsumer(textBoxConsumerGroup.Text, textBoxConsumerId.Text).Result;
                PrependMessage(createResponse);

                while (true)
                {
                    string content = _client.Consume(textBoxTopic.Text, textBoxConsumerGroup.Text, textBoxConsumerId.Text).Result;
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        PrependMessage("No messages, polling again");
                    }
                    else
                    {
                        PrependMessage(content);
                        PrependMessage("---");
                        string commitMessage = _client.CommitOffSet(textBoxConsumerGroup.Text, textBoxConsumerId.Text).Result;

                        var offsets = JsonConvert.DeserializeObject<List<Offset>>(commitMessage);
                        UpdateLable(offsets.Select(o => string.Format("{0}: {1}, {2}", o.Partition, o.Consumed, o.Committed)));
                    }
                }
            });
        }

        private void PrependMessage(string message)
        {
            if (textBoxMessages.InvokeRequired)
            {
                textBoxMessages.BeginInvoke((MethodInvoker)delegate
                {
                    textBoxMessages.Text = string.Format("{0}\r\n{1}", message, textBoxMessages.Text);
                });
            }
            else
            {
                textBoxMessages.Text = string.Format("{0}\r\n{1}", message, textBoxMessages.Text);
            }
        }

        private void UpdateLable(IEnumerable<string> offsets)
        {
            if (textBoxMessages.InvokeRequired)
            {
                textBoxMessages.BeginInvoke((MethodInvoker)delegate
                {
                    labelCount.Text = string.Join("\r\n", offsets);
                });
            }
            else
            {
                labelCount.Text = string.Join("\r\n", offsets);
            }
        }
    }

    public class Offset
    {
        [JsonProperty(PropertyName = "topic")]
        public string Topic { get; set; }

        [JsonProperty(PropertyName = "partition")]
        public int Partition { get; set; }

        [JsonProperty(PropertyName = "consumed")]
        public long Consumed { get; set; }

        [JsonProperty(PropertyName = "committed")]
        public long Committed { get; set; }
    }
}
