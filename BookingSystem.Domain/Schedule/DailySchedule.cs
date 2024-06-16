using BookingSystem.Domain.Extensions;

namespace BookingSystem.Domain.Schedule
{
    public class DailySchedule
    {
        private readonly DateOnly _date;
        private readonly DailyWorkSchedule _workSchedule;

        public DailySchedule(
            DateOnly date,
            DailyWorkSchedule workSchedule,
            IEnumerable<Slot> appointments,
            TimeSpan slotDuration)
        {
            _date = date;
            _workSchedule = workSchedule;

            DayName = _date.DayOfWeek;            
            Slots = ArrangeSlots(appointments, slotDuration);
            AvailableSlots = GetAvailableSlots();
        }

        public DayOfWeek DayName { get; } 
        public IEnumerable<Slot> Slots { get; }
        public IEnumerable<Slot> AvailableSlots { get; }

        private IEnumerable<Slot> GetAvailableSlots()
        {
            var availableSlots = Slots.Where(s => s.IsAvailable).ToList();

            return availableSlots;
        }
        
        private IEnumerable<Slot> ArrangeSlots(
            IEnumerable<Slot> appointments,
            TimeSpan slotDuration)
        {
            var slots = new List<Slot>();
            var activeWorkingTimeRanges = GetActiveWorkingTimeRanges();

            for(var i = 0; i < activeWorkingTimeRanges.Length; i++)
            {
                var startPeriod = activeWorkingTimeRanges[i].Start;
                var endPeriod = activeWorkingTimeRanges[i].End;

                var slotsInRange = GetSlotsBetweenTimeRange(
                    startPeriod,
                    endPeriod,
                    appointments,
                    slotDuration);

                slots.AddRange(slotsInRange);
            }

            return slots;
        }
        
        private static bool IsSlotAvailable(Slot slot, IEnumerable<Slot> appointments)
        {
            var isNotAvaible = appointments.Any(appointment =>                                        
                                        slot.Start >= appointment.Start
                                        && slot.End <= appointment.End);
            var isAvailable = !isNotAvaible;

            return isAvailable;
        }

        private (DateTime Start, DateTime End)[] GetActiveWorkingTimeRanges()
        {
            var activeWorkingTimeRanges = new (DateTime Start, DateTime End)[]
            {
                (_workSchedule.StartHour.ToDateTime(_date), _workSchedule.BreakStartHour.ToDateTime(_date)),
                (_workSchedule.BreakEndHour.ToDateTime(_date), _workSchedule.EndHour.ToDateTime(_date))
            };

            return activeWorkingTimeRanges;
        }

        private IEnumerable<Slot> GetSlotsBetweenTimeRange(
            DateTime startPeriod,
            DateTime endPeriod,
            IEnumerable<Slot> appointments,
            TimeSpan slotDuration)
        {
            var slots = new List<Slot>();

            for (var startSlot = startPeriod; startSlot < endPeriod; startSlot += slotDuration)
            {
                var endSlot = startSlot + slotDuration;
                if (endSlot > endPeriod)
                {
                    break;
                }

                var newSlot = new Slot
                {
                    Start = startSlot,
                    End = endSlot
                };
                newSlot.IsAvailable = IsSlotAvailable(newSlot, appointments);

                slots.Add(newSlot);
            }

            return slots;
        }
    }
}