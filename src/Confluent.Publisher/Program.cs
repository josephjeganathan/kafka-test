using System;
using System.Threading;
using System.Windows.Forms;
using Confluent.Client;

namespace Confluent.Publisher
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PublisherApp());
        }
    }
}
