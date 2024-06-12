using BookingSystem.Application.Mappings;
using BookingSystem.Application.Responses;
using BookingSystem.Application.Services;
using MediatR;

namespace BookingSystem.Application.Features.BookAppointments.Queries.GetWeeklySchedule
{
    public class GetWeeklyScheduleHandler : IRequestHandler<GetWeeklyScheduleQuery, Response<WeeklySlotResponse>>
    {
        private readonly IBookingService _bookingService;

        public GetWeeklyScheduleHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<Response<WeeklySlotResponse>> Handle(GetWeeklyScheduleQuery request, CancellationToken cancellationToken)
        {
            var date = DateOnly.Parse(request.Date);

            var weeklySchedule = await _bookingService.GetWeeklySchedule(date);                     

            var availableSlotsByDay = weeklySchedule.GetAvailableSlotsByDay();

            var weeklySlotResponse = availableSlotsByDay.MapToWeeklySlotResponse();

            var response = new Response<WeeklySlotResponse>(weeklySlotResponse);

            return response;
        }
    }
}