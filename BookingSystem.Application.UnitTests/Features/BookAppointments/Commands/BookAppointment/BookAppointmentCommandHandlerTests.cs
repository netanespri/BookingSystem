using AutoFixture.Xunit2;
using BookingSystem.Application.Features.BookAppointments.Commands.BookAppointment;
using BookingSystem.Application.Mappings;
using BookingSystem.Application.Responses;
using BookingSystem.Application.Services;
using BookingSystem.Domain.Appointment;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookingSystem.Application.UnitTests.Features.BookAppointments.Commands.BookAppointment
{
    public class BookAppointmentCommandHandlerTests
    {
        private readonly BookAppointmentCommandHandler _sut;
        private readonly Mock<IBookingService> _bookingServiceMock;
        private readonly Mock<ILogger<BookAppointmentCommandHandler>> _logger;

        public BookAppointmentCommandHandlerTests()
        {
            _bookingServiceMock = new Mock<IBookingService>();
            _logger = new Mock<ILogger<BookAppointmentCommandHandler>>();
            _sut = new BookAppointmentCommandHandler(_bookingServiceMock.Object, _logger.Object);
        }

        [Theory]
        [AutoData]
        public async Task When_Booking_Appointment_And_Slot_Avaible_Then_Return_Successful_Response(
            BookAppointmentCommand request)
        {
            // Assert
            var bookingServiceCanScheduleSlot = true;
            var expectedSuccessfulResponse = new Response<bool>(bookingServiceCanScheduleSlot);
            _bookingServiceMock.Setup(service => service.BookAppointment(It.IsAny<Appointment>()))
                               .ReturnsAsync(expectedSuccessfulResponse);

            // Act
            var response = await _sut.Handle(request, default);

            // Assert
            response.Should().NotBeNull();
            response.Succeeded.Should().Be(expectedSuccessfulResponse.Succeeded);
            response.Data.Should().Be(expectedSuccessfulResponse.Data);
            response.Message.Should().Be(expectedSuccessfulResponse.Message);
            _bookingServiceMock.Verify(b => b.BookAppointment(It.IsAny<Appointment>())
                                            ,Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task When_Booking_Appointment_And_Slot_Not_Available_Then_Return_Failed_Response(
            BookAppointmentCommand request)
        {
            // Assert
            var bookingServiceCannotScheduleSlot = false;
            var expectedResponse = new Response<bool>(
                data: bookingServiceCannotScheduleSlot,
                succeeded: bookingServiceCannotScheduleSlot,
                message: string.Empty);
            var expectedAppointment = request.ToAppointment();
            _bookingServiceMock.Setup(service => service.BookAppointment(It.IsAny<Appointment>()))
                               .ReturnsAsync(expectedResponse);

            // Act
            var response = await _sut.Handle(request, default);

            // Assert
            response.Should().NotBeNull();
            response.Succeeded.Should().Be(expectedResponse.Succeeded);
            response.Data.Should().Be(expectedResponse.Data);
            response.Message.Should().Be(expectedResponse.Message);
            _bookingServiceMock.Verify(b => b.BookAppointment(It.IsAny<Appointment>())
                                            , Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task When_Booking_Appointment_And_Service_Throws_Exception_Then_Throw_Exception(
            BookAppointmentCommand request)
        {
            // Assert
            _bookingServiceMock.Setup(service => service.BookAppointment(It.IsAny<Appointment>()))
                               .ThrowsAsync(new Exception());

            // Act
            Func<Task> act = async () => await _sut.Handle(request, default);

            // Assert
            await act.Should().ThrowAsync<Exception>();
            _bookingServiceMock.Verify(b => b.BookAppointment(It.IsAny<Appointment>())
                                            ,Times.Once);
        }
    }
}