namespace BookingSystem.Infrastructure.SlotService.Responses.WeeklyAvailabilityResponse
{
    public class WeeklyAvailabilityResponse
    {
        public Facility Facility { get; set; }
        public int SlotDurationMinutes { get; set; }
        public DayAvailability? Monday { get; set; }
        public DayAvailability? Tuesday { get; set; }
        public DayAvailability? Wednesday { get; set; }
        public DayAvailability? Thursday { get; set; }
        public DayAvailability? Friday { get; set; }
        public DayAvailability? Saturday { get; set; }
        public DayAvailability? Sunday { get; set; }        
    }
}