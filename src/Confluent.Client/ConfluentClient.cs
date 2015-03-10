using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Confluent.Client
{
    public class ConfluentClient
    {
        private readonly HttpClient _client;
        public ConfluentClient()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:8082")
            };
        }

        public async Task<string> Publish<TMessage>(string topic, params TMessage[] messages) where TMessage : class, new()
        {
            List<Record> records = messages.Select(message => new Record {Value = ToBase64(message)}).ToList();

            var content = new StringContent(JsonConvert.SerializeObject(new PublishRequest { Records = records }));
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/vnd.kafka.binary.v1+json");

            HttpResponseMessage responseMessage = await _client.PostAsync("/topics/" + topic, content);

            return await responseMessage.Content.ReadAsStringAsync();
        }

        private static string ToBase64<T>(T data)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)));
        }
    }

    public class PublishRequest
    {
        [JsonProperty(PropertyName = "records")]
        public List<Record> Records { get; set; }
    }

    public class Record
    {
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; } 
    }
}
