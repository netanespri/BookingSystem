using AutoFixture;
using BookingSystem.Domain.Schedule;

namespace BookingSystem.Application.UnitTests.Fixtures
{
    public class WeeklyScheduleFixture
    {
        private static Fixture _fixture = new Fixture();

        public static WeeklySchedule Get(string weekStartDate, bool hasWeeklyAvailableSlots)
        {
            var startDate = DateOnly.Parse(weekStartDate);
            var weeklySchedule = new WeeklySchedule
            {
                StartDate = startDate,
                Days = GetDays(startDate, hasWeeklyAvailableSlots)
            };

            return weeklySchedule;
        }

        private static IEnumerable<DailySchedule> GetDays(DateOnly startDate, bool hasWeeklyAvailableSlots)
        {
            var days = new List<DailySchedule>();

            for(var i = 0; i < 7; i++) 
            {
                var day = new DailySchedule(
                    date: startDate.AddDays(i),
                    workSchedule: _fixture.Create<DailyWorkSchedule>(),
                    appointments: _fixture.Create<IEnumerable<Slot>>(),
                    slotDuration: _fixture.Create<TimeSpan>()
                    );

                if (hasWeeklyAvailableSlots && day.Slots.Any())
                {
                    days.Add(day);
                }                

                UpdateDaySlotAvailability(day, hasWeeklyAvailableSlots);                
            }

            return days;
        }

        private static void UpdateDaySlotAvailability(DailySchedule day, bool enableWeeklyAvailableSlots)
        {
            var availableSlots = day.GetAvailableSlots();

            if (!enableWeeklyAvailableSlots)
            {
                foreach (var slot in day.Slots)
                {
                    slot.IsAvailable = false;
                }
            }
            else
            {
                if (!availableSlots.Any())
                {
                    var slot = day.Slots.FirstOrDefault();
                    if (slot != null)
                    {
                        slot.IsAvailable = true;
                    }
                }
            }
        }
    }
}