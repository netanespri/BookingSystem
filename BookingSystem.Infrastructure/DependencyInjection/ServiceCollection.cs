using BookingSystem.Application.Services;
using BookingSystem.Infrastructure.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Text;

namespace BookingSystem.Infrastructure.DependencyInjection
{
    public static class ServiceCollection
    {
        public static void AddInfrastructureLayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient<IHttpClientWrapper, HttpClientWrapper>()
            .ConfigureHttpClient(httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://draliatest.azurewebsites.net/api/availability/");
                httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", GetAuthorizationValue());
            });
            serviceCollection.AddScoped<IBookingService, SlotService.SlotService>();
        }

        private static string GetAuthorizationValue()
        {
            var byteArray = Encoding.ASCII.GetBytes("techuser:secretpassWord");
            var authValue = Convert.ToBase64String(byteArray);

            return authValue;
        }
    }        
}
