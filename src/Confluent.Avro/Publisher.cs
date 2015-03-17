using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Confluent.Avro
{
    public partial class Publisher : Form
    {
        private readonly AvroConfluentClient _client;
        private int _index;
        public Publisher()
        {
            InitializeComponent();
            _client = new AvroConfluentClient(ConfigurationManager.AppSettings["Confluent.BaseUrl"]);
        }

        private void buttonPublishWithSchema_Click(object sender, EventArgs e)
        {
            var random = new Random();
            Task.Run(() =>
            {
                int numberOfMessages = Convert.ToInt32(textBoxNumberOfMsg.Text);

                for (int i = 0; i < numberOfMessages; i++)
                {
                    _index++;

                    string name = string.Format("[{0}] {1}", _index, Guid.NewGuid().ToString("N"));
                    var person = new Person { Name = name, Age = random.Next(20, 100) };
                    PrependMessage(JsonConvert.SerializeObject(person));

                    string response = _client.PublishWithSchema(textBoxTopic.Text, new[] { person }).Result;
                    PrependMessage(response);
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
