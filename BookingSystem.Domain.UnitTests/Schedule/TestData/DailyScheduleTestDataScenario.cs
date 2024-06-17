using BookingSystem.Domain.Schedule;

namespace BookingSystem.Domain.UnitTests.Schedule.TestData
{
    public class DailyScheduleTestDataScenario
    {
        public DateOnly Date { get; set; }
        public DailyWorkSchedule DailyWorkSchedule { get; set; }
        public TimeSpan SlotDuration { get; set; }
        public IEnumerable<Slot> Appointments { get; set; }
        public IEnumerable<Slot> ExpectedSlots { get; set; }
        public IEnumerable<Slot> ExpectedAvailableSlots => ExpectedSlots.Where(slot => slot.IsAvailable).ToList();

        public static DateOnly CreateDefaultDate()
        {
            var date = DateOnly.FromDateTime(DateTime.Now);

            return date;
        }
    }
}
