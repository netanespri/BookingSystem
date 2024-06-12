namespace BookingSystem.Domain.Schedule
{
    public class DailyWorkSchedule
    {
        public TimeOnly StartHour { get; set; }
        public TimeOnly BreakStartHour { get; set; }
        public TimeOnly BreakEndHour { get; set; }
        public TimeOnly EndHour { get; set; }
    }
}