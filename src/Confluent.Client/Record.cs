using Newtonsoft.Json;

namespace Confluent.Client
{
    public class Record
    {
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; } 
    }
}