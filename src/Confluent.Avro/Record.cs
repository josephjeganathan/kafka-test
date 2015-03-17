using Newtonsoft.Json;

namespace Confluent.Avro
{
    public class Record<T> where T : class, new()
    {
        [JsonProperty(PropertyName = "value")]
        public T Value { get; set; } 
    }
}