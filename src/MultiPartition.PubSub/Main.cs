﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KafkaNet;
using KafkaNet.Model;
using Test.Common;
using KafkaMessage = KafkaNet.Protocol.Message;

namespace MultiPartition.PubSub
{
    public partial class Main : Form
    {
        private readonly List<Subscriber> _subscribers = new List<Subscriber>();
        private Producer _producer;
        private int i = 0;

        public Main()
        {
            InitializeComponent();
        }

        private void buttonConsumer_Click(object sender, EventArgs e)
        {
            var subscriber = new Subscriber();
            _subscribers.Add(subscriber);
            subscriber.Show();
        }

        private void buttonSendMessage_Click(object sender, EventArgs e)
        {
            i++;
            string message = string.Format("{0} - {1}", DateTime.UtcNow.ToString("O"), i);
            AppendMessage(message);
            _producer.SendMessageAsync("MultiPartition", new[] { new KafkaMessage(message) });
        }

        private void AppendMessage(string message)
        {
            if (textBoxMessage.InvokeRequired)
            {
                textBoxMessage.BeginInvoke((MethodInvoker)delegate
                {
                    textBoxMessage.Text += string.Format("\r\n{0}", message);
                });
            }
            else
            {
                textBoxMessage.Text += string.Format("\r\n{0}", message);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            var options = new KafkaOptions(new Uri("http://localhost:9092"))
            {
                Log = new ConsoleLog()
            };

            _producer = new Producer(new BrokerRouter(options));
        }
    }
}
