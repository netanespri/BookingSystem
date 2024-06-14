using BookingSystem.Domain.Schedule;
using BookingSystem.Infrastructure.SlotService.Responses.WeeklyAvailabilityResponse;

namespace BookingSystem.Infrastructure.Mappings
{
    public static class WeeklyAvailabilityResponseMapping
    {
        public static WeeklySchedule MapToWeeklySchedule(
            this WeeklyAvailabilityResponse weeklyAvailabilityResponse,
            DateOnly weekStartDate)
        {
            var weeklySchedule = new WeeklySchedule
            {
                FacilityId = weeklyAvailabilityResponse.Facility.FacilityId,
                StartDate = weekStartDate,
                Days = MapToDayAvailabilityList(weeklyAvailabilityResponse, weekStartDate)
            };

            return weeklySchedule;
        }

        private static IEnumerable<DailySchedule> MapToDayAvailabilityList(
            WeeklyAvailabilityResponse weeklyAvailabilityResponse,
            DateOnly weekStartDate)
        {
            var availableDays = new (DayAvailability? DayAvailability, DateOnly dayDate)[]
            {
                (weeklyAvailabilityResponse.Monday,    weekStartDate),
                (weeklyAvailabilityResponse.Tuesday,   weekStartDate.AddDays(1)),
                (weeklyAvailabilityResponse.Wednesday, weekStartDate.AddDays(2)),
                (weeklyAvailabilityResponse.Thursday,  weekStartDate.AddDays(3)),
                (weeklyAvailabilityResponse.Friday,    weekStartDate.AddDays(4)),
                (weeklyAvailabilityResponse.Saturday,  weekStartDate.AddDays(5)),
                (weeklyAvailabilityResponse.Sunday,    weekStartDate.AddDays(6))
            };
            var days = availableDays.Where(dayTuple => dayTuple.DayAvailability != default(DayAvailability))
                                    .Select(dayTuple => 
                                                  MapToDayAvailability(
                                                        weeklyAvailabilityResponse, 
                                                        dayTuple.DayAvailability, 
                                                        dayTuple.dayDate));

            return days;
        }

        private static DailySchedule MapToDayAvailability(
            WeeklyAvailabilityResponse weeklyAvailabilityResponse,
            DayAvailability dayAvailability,
            DateOnly dayDate)
        {
            var slotDuration = TimeSpan.FromMinutes(weeklyAvailabilityResponse.SlotDurationMinutes);
            var workSchedule = MapToDailyWorkSchedule(dayAvailability.WorkPeriod);
            var appointments = MapToAppointments(dayAvailability.BusySlots);
                
            var dailySchedule = new DailySchedule(
                dayDate,
                workSchedule,
                appointments,
                slotDuration);

            return dailySchedule;
        }

        private static DailyWorkSchedule MapToDailyWorkSchedule(WorkPeriod workPeriod)
        {
            var dailyWorkSchedule = new DailyWorkSchedule
            {
                StartHour = new TimeOnly(workPeriod.StartHour, 0),
                BreakStartHour = new TimeOnly(workPeriod.LunchStartHour, 0),
                BreakEndHour = new TimeOnly(workPeriod.LunchEndHour, 0),
                EndHour = new TimeOnly(workPeriod.EndHour, 0)
            };

            return dailyWorkSchedule;
        }

        private static IEnumerable<Domain.Schedule.Slot> MapToAppointments(
            IEnumerable<SlotService.Responses.WeeklyAvailabilityResponse.Slot>? busySlots)
        {
            var appointments = busySlots?
                                .Select(p => new { p.Start, p.End })
                                .Distinct()
                                .Select(slot => new Domain.Schedule.Slot
                                {
                                    Start = slot.Start,
                                    End = slot.End
                                })
                                .ToArray();
            
            return appointments ?? Enumerable.Empty<Domain.Schedule.Slot>();
        }
    }
}