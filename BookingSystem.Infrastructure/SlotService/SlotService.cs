using BookingSystem.Domain.Schedule;
using BookingSystem.Infrastructure.Http;
using BookingSystem.Infrastructure.Mappings;
using BookingSystem.Infrastructure.SlotService.Responses.WeeklyAvailabilityResponse;
using Microsoft.Extensions.Logging;
using BookingSystem.Application.Services;

namespace BookingSystem.Infrastructure.SlotService
{
    public class SlotService : IBookingService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly ILogger _logger;

        public SlotService(IHttpClientWrapper httpClientWrapper, ILogger<SlotService> logger)
        {
            _httpClientWrapper = httpClientWrapper;
            _logger = logger;
        }

        public async Task<WeeklySchedule> GetWeeklySchedule(DateOnly date)
        {
            string url = BuildUrl(date);

            // Call slot service
            var response = await _httpClientWrapper.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();
            //            
            var apiResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<WeeklyAvailabilityResponse>(content);

            var weeklySchedule = apiResponse.MapToWeeklySchedule(date);

            _logger.LogDebug($"{nameof(SlotService)} got response.", response);

            return weeklySchedule;
        }

        private static string BuildUrl(DateOnly date)
        {
            var dateStr = date.ToString(@"yyyyMMdd");
            string url = $"GetWeeklyAvailability/{dateStr}";

            return url;
        }
    }
}
