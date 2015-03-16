using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Confluent.Client;
using Newtonsoft.Json;

namespace Confluent.Publisher
{
    public partial class PublisherApp : Form
    {
        private readonly List<Process> _processes = new List<Process>();
        private readonly ConfluentClient _client;
        private int _index;

        public PublisherApp()
        {
            InitializeComponent();
            _client = new ConfluentClient(ConfigurationManager.AppSettings["Confluent.BaseUrl"]);
        }

        private void textBoxNumberOfMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void buttonPublish_Click(object sender, EventArgs e)
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

                    string response = _client.Publish(textBoxTopic.Text, new[] { person }).Result;
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

        private void buttonCreateConsumer_Click(object sender, EventArgs e)
        {
            Process process = Process.Start(Path.GetFullPath("../../../Confluent.Consumer/bin/Debug/Confluent.Consumer.exe"));
            _processes.Add(process);
        }

        private void PublisherApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Process process in _processes)
            {
                try
                {
                    process.Kill();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
