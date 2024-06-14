using BookingSystem.Application.Mappings;
using BookingSystem.Application.Responses;
using BookingSystem.Application.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Application.Features.BookAppointments.Queries.GetWeeklySchedule
{
    public class GetWeeklyScheduleHandler : IRequestHandler<GetWeeklyScheduleQuery, Response<WeeklySlotResponse>>
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger _logger;

        public GetWeeklyScheduleHandler(IBookingService bookingService, ILogger<GetWeeklyScheduleHandler> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        public async Task<Response<WeeklySlotResponse>> Handle(GetWeeklyScheduleQuery request, CancellationToken cancellationToken)
        {
            var date = DateOnly.Parse(request.Date);

            var weeklyScheduleResponse = await _bookingService.GetWeeklySchedule(date);                     

            var weeklySlotResponse = weeklyScheduleResponse.Data.MapToWeeklySlotResponse();
                        
            _logger.LogInformation($"{nameof(WeeklySlotResponse)} received for facility Id: {weeklySlotResponse.FacilityId}." +
                                   $"Total available slots: {weeklySlotResponse.DailyAvailableSlots.Count}");

            var response = new Response<WeeklySlotResponse>(
                data: weeklySlotResponse, 
                succeeded: weeklyScheduleResponse.Succeeded,
                message: weeklyScheduleResponse.Message);

            return response;
        }
    }
}