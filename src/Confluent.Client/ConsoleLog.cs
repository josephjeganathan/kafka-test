using System;

namespace Confluent.Client
{
    public class ConsoleLog
    {
        public static void Log(string message)
        {
            if (_logNoice) Console.WriteLine(message);
        }

        public static void WaitOnKeys()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Modifiers == ConsoleModifiers.Control)
                {
                    if (keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.C) break;
                    if (keyInfo.Key == ConsoleKey.L) Console.Clear();
                    if (keyInfo.Key == ConsoleKey.T) ToggleNoice();
                }
            }
        }

        private static bool _logNoice = true;
        private static void ToggleNoice()
        {
            _logNoice = !_logNoice;
        }
    }
}
