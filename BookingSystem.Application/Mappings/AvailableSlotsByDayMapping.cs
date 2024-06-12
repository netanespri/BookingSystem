using BookingSystem.Application.Features.BookAppointments.Queries.GetWeeklySchedule;
using BookingSystem.Domain.Schedule;

namespace BookingSystem.Application.Mappings
{
    public static class AvailableSlotsByDayMapping
    {
        public static WeeklySlotResponse MapToWeeklySlotResponse(
            this Dictionary<DayOfWeek, IEnumerable<Slot>> availableSlotsByDay)
        {
            var weeklySlotResponse = new WeeklySlotResponse
            {
                DailySlots = availableSlotsByDay.ToDictionary(
                                        slotPairs => slotPairs.Key,
                                        slotPairs => slotPairs.Value.Select(
                                            slot => new SlotResponse
                                            {
                                                Start = slot.Start,
                                                End = slot.End
                                            }))
            };

            return weeklySlotResponse;
        }
    }
}
