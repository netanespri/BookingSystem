namespace BookingSystem.Domain.Schedule
{
    public class Slot
    {
        public bool IsAvailable { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}