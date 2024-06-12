using BookingSystem.Application.Responses;
using MediatR;

namespace BookingSystem.Application.Features.BookAppointments.Queries.GetWeeklySchedule
{
    public class GetWeeklyScheduleQuery : IRequest<Response<WeeklySlotResponse>>
    {
        public string Date { get; set; }
    }
}
