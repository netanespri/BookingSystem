namespace BookingSystem.Application.Features.BookAppointments.Queries.GetWeeklySchedule
{
    public class WeeklySlotResponse
    {
        public Guid FacilityId { get; set; }
        public Dictionary<DayOfWeek, IEnumerable<SlotResponse>> DailyAvailableSlots { get; set; } = new Dictionary<DayOfWeek, IEnumerable<SlotResponse>>();
    }
}
