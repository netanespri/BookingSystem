using BookingSystem.Domain.Schedule;
using BookingSystem.Domain.UnitTests.Schedule.TestData;
using FluentAssertions;

namespace BookingSystem.Domain.UnitTests.Schedule.Assertions
{
    public static class DailyScheduleAssertions
    {
        public static void Assert(
            this DailySchedule dailySchedule,
            DailyScheduleTestDataScenario dailyScheduleTestDataScenario)
        {
            dailySchedule.Slots.Should().NotBeNull();
            dailySchedule.Slots.Should().BeEquivalentTo(dailyScheduleTestDataScenario.ExpectedSlots);
            AssertAllSlotsMatchExpectedDuration(dailySchedule.Slots, dailyScheduleTestDataScenario.SlotDuration);
            dailySchedule.AvailableSlots.Should().BeEquivalentTo(dailyScheduleTestDataScenario.ExpectedAvailableSlots);
        }

        private static void AssertAllSlotsMatchExpectedDuration(IEnumerable<Slot> slots, TimeSpan slotDuration)
        {
            slots.All(slot => (slot.End - slot.Start).Minutes == slotDuration.Minutes)
                  .Should().BeTrue();
        }
    }
}
