namespace BookingSystem.Domain.Schedule
{
    public class WeeklySchedule
    {        
        public DateOnly StartDate { get; set; }
        public IEnumerable<DailySchedule> Days { get; set; } = Enumerable.Empty<DailySchedule>();

        public Dictionary<DayOfWeek, IEnumerable<Slot>> GetAvailableSlotsByDay()
        {
            var availableSlots = Days.ToDictionary(
                                        daySchedule => daySchedule.DayName,
                                        daySchedule => daySchedule.GetAvailableSlots());

            return availableSlots;
        }
    }
}