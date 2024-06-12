namespace BookingSystem.Application.Features.BookAppointments.Queries.GetWeeklySchedule
{
    public class WeeklySlotResponse
    {
        // Represents the available slots per day
        // Key: day name
        // Value: list of slots.
        public Dictionary<DayOfWeek, IEnumerable<SlotResponse>> DailySlots { get; set; } = new Dictionary<DayOfWeek, IEnumerable<SlotResponse>>();
    }
}
