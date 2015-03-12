using System.Collections.Generic;
using Newtonsoft.Json;

namespace Confluent.Client
{
    public class PublishRequest
    {
        [JsonProperty(PropertyName = "records")]
        public List<Record> Records { get; set; }
    }
}