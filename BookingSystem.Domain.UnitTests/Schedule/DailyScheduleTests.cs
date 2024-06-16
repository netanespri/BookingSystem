using AutoFixture.Xunit2;
using BookingSystem.Domain.Schedule;
using BookingSystem.Domain.UnitTests.Schedule.TestData;
using FluentAssertions;

namespace BookingSystem.Domain.UnitTests.Schedule
{
    public class DailyScheduleTests
    {
        [Theory]
        [AutoData]
        public void When_Daily_Schedule_Created_Then_The_Day_Is_Set(
            DateTime dateTime,
            DailyWorkSchedule dailyWorkSchedule,
            IEnumerable<Slot> appointments,
            TimeSpan slotDuration)
        {
            // Arrange
            var expectedDate = DateOnly.FromDateTime(dateTime);

            // Act
            var sut = new DailySchedule(
                                expectedDate,
                                dailyWorkSchedule,
                                appointments,
                                slotDuration);

            // Assert
            sut.DayName.Should().Be(expectedDate.DayOfWeek);
        }

        [Theory]
        [ClassData(typeof(DailyScheduleTestData))]
        public void When_Daily_Schedule_Created_Then_Slots_Are_Arranged_With_Expected_Availability(
            DailyScheduleTestDataScenario dailyScheduleTestDataScenario)
        {
            // Act
            var sut = new DailySchedule(
                                dailyScheduleTestDataScenario.Date,
                                dailyScheduleTestDataScenario.DailyWorkSchedule,
                                dailyScheduleTestDataScenario.Appointments,
                                dailyScheduleTestDataScenario.SlotDuration);

            // Assert
            sut.Slots.Should().NotBeNull();
            sut.Slots.Should().BeEquivalentTo(dailyScheduleTestDataScenario.ExpectedSlots);
            AssertAllSlotsMatchExpectedDuration(sut.Slots, dailyScheduleTestDataScenario.SlotDuration);
            sut.AvailableSlots.Should().BeEquivalentTo(dailyScheduleTestDataScenario.ExpectedAvailableSlots);
        }
               
        private static void AssertAllSlotsMatchExpectedDuration(IEnumerable<Slot> slots, TimeSpan slotDuration)
        {
            slots.All(slot => (slot.End - slot.Start).Minutes == slotDuration.Minutes)
                  .Should().BeTrue();
        }
    }    
}
