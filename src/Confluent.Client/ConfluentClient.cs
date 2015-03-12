using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Confluent.Client
{
    public class ConfluentClient
    {
        private const string ContentType = "application/vnd.kafka.binary.v1+json";
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

            string requestUri = "/topics/" + topic;
            string content = JsonConvert.SerializeObject(new PublishRequest { Records = records });
            HttpResponseMessage responseMessage = await SendRequest(HttpMethod.Post, requestUri, content);

            return await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateConsumer(string consumerGroup, string consumerId)
        {
            string requestUri = string.Format("/consumers/{0}/", consumerGroup);
            string content = JsonConvert.SerializeObject(new CreateConsumerRequest { Id = consumerId });
            HttpResponseMessage responseMessage = await SendRequest(HttpMethod.Post, requestUri, content);

            return await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<string> Consume(string topic, string consumerGroup, string consumerId)
        {
            string requestUri = string.Format("/consumers/{0}/instances/{1}/topics/{2}", consumerGroup, consumerId, topic);
            HttpResponseMessage responseMessage = await SendRequest(HttpMethod.Get, requestUri);

            if (responseMessage.IsSuccessStatusCode)
            {
                return FromBase64(await responseMessage.Content.ReadAsStringAsync());
            }

            return await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<string> CommitOffSet(string consumerGroup, string consumerId)
        {
            string requestUri = string.Format("/consumers/{0}/instances/{1}/offsets", consumerGroup, consumerId);
            HttpResponseMessage responseMessage = await SendRequest(HttpMethod.Post, requestUri);

            return await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteConsumer(string consumerGroup, string consumerId)
        {
            string requestUri = string.Format("/consumers/{0}/instances/{1}", consumerGroup, consumerId);
            HttpResponseMessage responseMessage = await SendRequest(HttpMethod.Delete, requestUri);

            return await responseMessage.Content.ReadAsStringAsync();
        }

        private async Task<HttpResponseMessage> SendRequest(HttpMethod method, string requestUri, string content = null)
        {
            var request = new HttpRequestMessage(method, requestUri);

            if (content != null)
            {
                request.Content = new StringContent(content, Encoding.UTF8, ContentType);
            }
            else
            {
                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));                
            }

            return await _client.SendAsync(request);
        }

        private static string ToBase64<T>(T data)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)));
        }

        private static string FromBase64(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return data;
            }

            var messageLogs = JsonConvert.DeserializeObject<List<MessageLog>>(data);

            if (!messageLogs.Any())
            {
                return null;
            }

            IEnumerable<string> logs = messageLogs.Select(ml => Encoding.UTF8.GetString(Convert.FromBase64String(ml.Value)));

            return string.Join("\r\n", logs);
        }
    }
}
