using System.Text;
using System.Text.Json;

namespace BookingSystem.Infrastructure.Http
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> GetAsync(string requestUrl)
        {
            var response = await _httpClient.GetAsync(requestUrl);

            return response;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string requestUrl, T request) where T : class
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(requestUrl, content);

            return response;
        }
    }
}