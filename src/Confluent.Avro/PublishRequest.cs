using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Confluent.Avro
{
    public class PublishRequest<T> where T : class, new()
    {
        [JsonProperty(PropertyName = "value_schema")]
        public string Schema { get; set; }

        [JsonProperty(PropertyName = "records")]
        public List<Record<T>> Records { get; set; }
    }
}
