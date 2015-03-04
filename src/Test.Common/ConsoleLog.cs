using System;
using KafkaNet;

namespace Test.Common
{
    public class ConsoleLog : IKafkaLog
    {
        public void DebugFormat(string format, params object[] args)
        {
            if (_logNoice) Console.WriteLine(format, args);
        }

        public void InfoFormat(string format, params object[] args)
        {
            if (_logNoice) Console.WriteLine(format, args);
        }

        public void WarnFormat(string format, params object[] args)
        {
            if (_logNoice) Console.WriteLine(format, args);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            if (_logNoice) Console.WriteLine(format, args);
        }

        public void FatalFormat(string format, params object[] args)
        {
            if (_logNoice) Console.WriteLine(format, args);
        }

        public static void WaitOnKeys()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Modifiers == ConsoleModifiers.Control)
                {
                    if (keyInfo.Key == ConsoleKey.D) break;
                    if (keyInfo.Key == ConsoleKey.L) Console.Clear();
                    if (keyInfo.Key == ConsoleKey.T) ToggleNoice();
                }
            }
        }

        private static bool _logNoice = false;
        private static void ToggleNoice()
        {
            _logNoice = !_logNoice;
        }
    }
}
