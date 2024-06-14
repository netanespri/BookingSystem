using BookingSystem.Application.Mappings;
using BookingSystem.Application.Responses;
using BookingSystem.Application.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Application.Features.BookAppointments.Commands.BookAppointment
{
    public class BookAppointmentCommandHandler : IRequestHandler<BookAppointmentCommand, Response<bool>>
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger _logger;

        public BookAppointmentCommandHandler(IBookingService bookingService, ILogger<BookAppointmentCommandHandler> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }
        public async Task<Response<bool>> Handle(BookAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = request.ToAppointment();

            var response = await _bookingService.BookAppointment(appointment);

            _logger.LogInformation($"Response received. Succeeded: {response.Succeeded}, Message: {response.Message}");

            return response;
        }
    }
}