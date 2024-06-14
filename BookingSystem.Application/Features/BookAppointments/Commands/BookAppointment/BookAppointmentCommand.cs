using BookingSystem.Application.Responses;
using MediatR;

namespace BookingSystem.Application.Features.BookAppointments.Commands.BookAppointment
{
    public class BookAppointmentCommand : IRequest<Response<bool>>
    {
        public Guid FacilityId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Comments { get; set; } = string.Empty;
        public Patient Patient { get; set; } = new Patient();
    }
}