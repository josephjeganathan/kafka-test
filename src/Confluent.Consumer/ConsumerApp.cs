using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using Confluent.Client;

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
            buttonStart.Enabled = false;

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
                        string commitMessage = _client.CommitOffSet(textBoxConsumerGroup.Text, textBoxConsumerId.Text).Result;
                        PrependMessage(commitMessage);
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
    }
}
