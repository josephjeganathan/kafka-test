using System;
using KafkaNet;

namespace TestLog.Loader
{
    public class NoLog : IKafkaLog
    {
        public void DebugFormat(string format, params object[] args)
        {
        }

        public void InfoFormat(string format, params object[] args)
        {
        }

        public void WarnFormat(string format, params object[] args)
        {
        }

        public void ErrorFormat(string format, params object[] args)
        {
        }

        public void FatalFormat(string format, params object[] args)
        {
        }
    }
}
