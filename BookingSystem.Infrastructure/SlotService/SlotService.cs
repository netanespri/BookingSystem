using BookingSystem.Domain.Schedule;
using BookingSystem.Infrastructure.Http;
using BookingSystem.Infrastructure.Mappings;
using BookingSystem.Infrastructure.SlotService.Responses.WeeklyAvailabilityResponse;
using Microsoft.Extensions.Logging;
using BookingSystem.Application.Services;
using BookingSystem.Domain.Appointment;
using System.Text.Json;
using BookingSystem.Application.Responses;

namespace BookingSystem.Infrastructure.SlotService
{
    public class SlotService : IBookingService
    {
        private const string GetWeeklyAvailabilityUrlPath = "GetWeeklyAvailability/";
        private const string BookAppointmentUrlPath = "TakeSlot";

        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly ILogger _logger;

        public SlotService(IHttpClientWrapper httpClientWrapper, ILogger<SlotService> logger)
        {
            _httpClientWrapper = httpClientWrapper;
            _logger = logger;
        }

        public async Task<Response<WeeklySchedule>> GetWeeklySchedule(DateOnly date)
        {
            string url = BuildGetWeeklyAvailabilityUrl(date);

            var content = await GetHttpResponseContent(url);
            var apiResponse = JsonSerializer.Deserialize<WeeklyAvailabilityResponse>(content);

            _logger.LogDebug($"{nameof(SlotService)} got response.", apiResponse);

            var weeklySchedule = apiResponse?.MapToWeeklySchedule(date) ?? 
                                 new WeeklySchedule();          

            return new Response<WeeklySchedule>(weeklySchedule);
        }        

        public async Task<Response<bool>> BookAppointment(Appointment appointment)
        {
            string url = BookAppointmentUrlPath;

            var apiResponse = await _httpClientWrapper.PostAsync(url, appointment);

            _logger.LogDebug($"{nameof(SlotService)} got response.", apiResponse);

            var responseString = await apiResponse.Content.ReadAsStringAsync();

            return new Response<bool>(
                data: apiResponse.IsSuccessStatusCode,
                succeeded: apiResponse.IsSuccessStatusCode,
                message: responseString);
        }

        private async Task<string> GetHttpResponseContent(string url)
        {
            var response = await _httpClientWrapper.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();

            return content;
        }

        private static string BuildGetWeeklyAvailabilityUrl(DateOnly date)
        {
            var dateStr = date.ToString(@"yyyyMMdd");
            string url = $"{GetWeeklyAvailabilityUrlPath}{dateStr}";

            return url;
        }
    }
}
