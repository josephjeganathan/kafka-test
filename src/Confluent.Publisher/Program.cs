using System;
using System.Threading;
using Confluent.Client;

namespace Confluent.Publisher
{
    class Program
    {
        private const string Topic = "SinglePartition";

        static void Main(string[] args)
        {
            try
            {
                var client = new ConfluentClient();
                int i = 0;
                var random = new Random();
                while (true)
                {
                    i++;
                    string name = string.Format("[{0}] {1}", i, Guid.NewGuid().ToString("N"));
                    string response = client.Publish(Topic, new[] { new Person { Name = name, Age = random.Next(20, 100) } }).Result;

                    Console.ForegroundColor = ConsoleColor.Green;
                    ConsoleLog.Log(response);
                    Console.ForegroundColor = ConsoleColor.White;
                    
                    Thread.Sleep(random.Next(0, 10));
                }
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
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
