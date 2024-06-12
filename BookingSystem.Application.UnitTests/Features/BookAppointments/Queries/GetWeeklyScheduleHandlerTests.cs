using BookingSystem.Application.Features.BookAppointments.Queries.GetWeeklySchedule;
using BookingSystem.Application.Services;
using BookingSystem.Application.UnitTests.Fixtures;
using FluentAssertions;
using Moq;

namespace BookingSystem.Core.UnitTests.Features.BookAppointments.Queries
{
    public class GetWeeklyScheduleHandlerTests
    {
        private const string WeekStartDate = "2024-06-10";
        private readonly GetWeeklyScheduleHandler _sut;
        private readonly Mock<IBookingService> _bookingServiceMock;
        
        public GetWeeklyScheduleHandlerTests()
        {
            _bookingServiceMock = new Mock<IBookingService>();
            _sut = new GetWeeklyScheduleHandler(_bookingServiceMock.Object);
        }

        // Given_Correct_Date_Format_And_Available_Slots_When_Handling_Request_Then_Return_Weekly_Available_Slots
        // Given_Correct_Date_Format_And_No_Available_Slots_When_Handling_Request_Then_Return_No_Available_Slots
        // Given_Incorrect_Date_Format_And_No_Validation_Behaviour_When_Handling_Request_Then_Throw_Exception


        [Fact]
        public async Task Given_Correct_Date_Format_And_Available_Slots_When_Handling_Request_Then_Return_Weekly_Available_Slots()
        {
            // Arrange
            var weeklySchedule = WeeklyScheduleFixture.Get(
                weekStartDate: WeekStartDate,
                enableWeeklyAvailableSlots: true);
            var expectedDailySlots = 
                weeklySchedule.GetAvailableSlotsByDay().ToDictionary(
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
            _bookingServiceMock.Setup(service => service.GetWeeklySchedule(weeklySchedule.StartDate))
                               .ReturnsAsync(weeklySchedule);

            // Act
            var response = await _sut.Handle(request, default);

            // Assert
            response.Should().NotBeNull();
            response.Succeeded.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Data.DailySlots.Should().NotBeNull();
            response.Data.DailySlots.Should().BeEquivalentTo(expectedDailySlots);
        }

        [Fact]
        public async Task Given_Correct_Date_Format_And_No_Available_Slots_When_Handling_Request_Then_Return_No_Available_Slots()
        {
            // Arrange
            var weeklySchedule = WeeklyScheduleFixture.Get(
                weekStartDate: WeekStartDate,
                enableWeeklyAvailableSlots: false);                 

            var request = new GetWeeklyScheduleQuery
            {
                Date = weeklySchedule.StartDate.ToString()
            };
            _bookingServiceMock.Setup(service => service.GetWeeklySchedule(weeklySchedule.StartDate))
                               .ReturnsAsync(weeklySchedule);

            // Act
            var response = await _sut.Handle(request, default);

            // Assert
            response.Should().NotBeNull();
            response.Succeeded.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Data.DailySlots.Should().NotBeNull();
            response.Data.DailySlots.Should().BeEmpty();
        }

        [Theory]
        [InlineData("")]
        [InlineData("asd")]
        [InlineData("2015-05-16T05:50:06.7199222-04:00")]
        public async Task Given_Incorrect_Date_Format_And_No_Validation_Behaviour_When_Handling_Request_Then_Throw_FormatException(
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