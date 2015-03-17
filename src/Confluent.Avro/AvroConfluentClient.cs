using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Confluent.Avro
{
    public class AvroConfluentClient
    {
        private const string ContentType = "application/vnd.kafka.avro.v1+json";
        private readonly HttpClient _client;

        public AvroConfluentClient(string baseUrl)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public async Task<string> PublishWithSchema(string topic, params Person[] people)
        {
            string requestUri = "/topics/" + topic;
            var request = new PublishRequest<Person>
            {
                Records = people.Select(p => new Record<Person>{Value = p}).ToList(),
                Schema = Person.Schema
            };

            string content = JsonConvert.SerializeObject(request);
            HttpResponseMessage responseMessage = await SendRequest(HttpMethod.Post, requestUri, content);

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
    }
}
