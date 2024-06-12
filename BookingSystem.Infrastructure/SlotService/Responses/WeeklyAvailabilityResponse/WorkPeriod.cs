namespace BookingSystem.Infrastructure.SlotService.Responses.WeeklyAvailabilityResponse
{
    public class WorkPeriod
    {
        public int StartHour { get; set; }
        public int LunchStartHour { get; set; }
        public int LunchEndHour { get; set; }
        public int EndHour { get; set; }
    }
}
