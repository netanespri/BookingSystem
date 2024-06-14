using BookingSystem.Application.Features.BookAppointments.Queries.GetWeeklySchedule;
using BookingSystem.Domain.Schedule;

namespace BookingSystem.Application.Mappings
{
    public static class WeeklyScheduleMapping
    {
        public static WeeklySlotResponse MapToWeeklySlotResponse(this WeeklySchedule weeklySchedule)
        {
            var weeklySlotResponse = new WeeklySlotResponse
            {
                FacilityId = weeklySchedule.FacilityId, 
                DailyAvailableSlots = weeklySchedule.AvailableSlotsByDay.ToDictionary(
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
