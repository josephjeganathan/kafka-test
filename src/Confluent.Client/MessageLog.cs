namespace Confluent.Client
{
    public class MessageLog
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int Partition { get; set; }
        public long Offset { get; set; }
    }
}
