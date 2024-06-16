namespace BookingSystem.Domain.Schedule
{
    public class WeeklySchedule
    {
        public Guid FacilityId { get; set; }
        public DateOnly StartDate { get; set; }
        public IEnumerable<DailySchedule> Days { get; set; } = Enumerable.Empty<DailySchedule>();

        public Dictionary<DayOfWeek, IEnumerable<Slot>> AvailableSlotsByDay => 
            Days
            .Where(daySchedule => daySchedule.AvailableSlots.Any())
            .ToDictionary(daySchedule => daySchedule.DayName,
                                              daySchedule => daySchedule.AvailableSlots);
    }
}