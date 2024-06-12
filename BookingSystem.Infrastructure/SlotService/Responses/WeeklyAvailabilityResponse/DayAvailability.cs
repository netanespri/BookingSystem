using System;
namespace BookingSystem.Infrastructure.SlotService.Responses.WeeklyAvailabilityResponse
{
    public class DayAvailability
    {
        public WorkPeriod WorkPeriod { get; set; } = new WorkPeriod();
        public IEnumerable<Slot>? BusySlots { get; set; }
    }
}
