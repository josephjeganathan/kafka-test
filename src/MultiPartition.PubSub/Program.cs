using System;
using System.Windows.Forms;

namespace MultiPartition.PubSub
{
    /// <summary>
    /// Multi Partition topic
    /// bin/kafka-topics.sh --create --zookeeper localhost:2181 --replication-factor 1 --partitions 3 --topic MultiPartition
    /// </summary>
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
