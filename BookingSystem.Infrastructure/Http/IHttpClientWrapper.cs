namespace BookingSystem.Infrastructure.Http
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> GetAsync(string requestUrl);
        Task<HttpResponseMessage> PostAsync<T>(string requestUrl, T request) where T : class;
    }
}
