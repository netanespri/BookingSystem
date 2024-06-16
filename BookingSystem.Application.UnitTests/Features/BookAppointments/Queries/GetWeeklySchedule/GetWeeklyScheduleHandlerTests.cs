using BookingSystem.Application.Features.BookAppointments.Queries.GetWeeklySchedule;
using BookingSystem.Application.Responses;
using BookingSystem.Application.Services;
using BookingSystem.Application.UnitTests.Fixtures;
using BookingSystem.Domain.Schedule;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookingSystem.Application.UnitTests.Features.BookAppointments.Queries.GetWeeklySchedule
{
    public class GetWeeklyScheduleHandlerTests
    {
        private const string WeekStartDate = "2024-06-10";
        private readonly GetWeeklyScheduleHandler _sut;
        private readonly Mock<IBookingService> _bookingServiceMock;
        private readonly Mock<ILogger<GetWeeklyScheduleHandler>> _logger;

        public GetWeeklyScheduleHandlerTests()
        {
            _bookingServiceMock = new Mock<IBookingService>();
            _logger = new Mock<ILogger<GetWeeklyScheduleHandler>>();
            _sut = new GetWeeklyScheduleHandler(_bookingServiceMock.Object, _logger.Object);
        }

        [Fact]
        public async Task When_Requesting_Weekly_Slots_And_Available_Slots_Exists_Then_Return_Slots()
        {
            // Arrange
            var weeklySchedule = WeeklyScheduleFixture.Get(
                weekStartDate: WeekStartDate,
                hasWeeklyAvailableSlots: true);
            var expectedDailySlots =
                weeklySchedule.AvailableSlotsByDay.ToDictionary(
                                        daySchedule => daySchedule.Key,
                                        daySchedule => daySchedule.Value.Select(slot => new SlotResponse
                                        {
                                            Start = slot.Start,
                                            End = slot.End
                                        }));
            var request = new GetWeeklyScheduleQuery
            {
                Date = weeklySchedule.StartDate.ToString()
            };
            var weeklyScheduleResponse = new Response<WeeklySchedule>(weeklySchedule);
            _bookingServiceMock.Setup(service => service.GetWeeklySchedule(weeklySchedule.StartDate))
                               .ReturnsAsync(weeklyScheduleResponse);

            // Act
            var response = await _sut.Handle(request, default);

            // Assert
            response.Should().NotBeNull();
            response.Succeeded.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Data.DailyAvailableSlots.Should().NotBeNull();
            response.Data.DailyAvailableSlots.Should().BeEquivalentTo(expectedDailySlots);
        }

        [Fact]
        public async Task When_Requesting_Weekly_Slots_And_No_Available_Slots_Then_Return_No_Slots()
        {
            // Arrange
            var weeklySchedule = WeeklyScheduleFixture.Get(
                weekStartDate: WeekStartDate,
                hasWeeklyAvailableSlots: false);

            var request = new GetWeeklyScheduleQuery
            {
                Date = weeklySchedule.StartDate.ToString()
            };
            var weeklyScheduleResponse = new Response<WeeklySchedule>(weeklySchedule);
            _bookingServiceMock.Setup(service => service.GetWeeklySchedule(weeklySchedule.StartDate))
                               .ReturnsAsync(weeklyScheduleResponse);

            // Act
            var response = await _sut.Handle(request, default);

            // Assert
            response.Should().NotBeNull();
            response.Succeeded.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Data.DailyAvailableSlots.Should().NotBeNull();
            response.Data.DailyAvailableSlots.Should().BeEmpty();
        }

        // This scenario is not applicable at runtime when the MediatR validation pipeline is running.
        // Hence, this unit test does not use the validation pipeline that is executed at runtime before the handler takes the request. 
        [Theory]
        [InlineData("")]
        [InlineData("asd")]
        [InlineData("2015-05-16T05:50:06.7199222-04:00")]
        public async Task When_Requesting_Weekly_Slots_And_Incorrect_Date_Format_Then_Throw_FormatException(
            string weekStartDate)
        {
            // Arrange
            var request = new GetWeeklyScheduleQuery
            {
                Date = weekStartDate
            };

            // Act
            Func<Task> act = async () => await _sut.Handle(request, default);

            // Assert
            await act.Should().ThrowAsync<FormatException>();
            _bookingServiceMock.Verify(
                b => b.GetWeeklySchedule(It.IsAny<DateOnly>()),
                Times.Never);
        }
    }
}