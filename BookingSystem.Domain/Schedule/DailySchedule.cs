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
            Slots = BuildSlots(appointments, slotDuration);
        }

        public DayOfWeek DayName { get; } 
        public IEnumerable<Slot> Slots { get; }

        public IEnumerable<Slot> GetAvailableSlots()
        {
            var availableSlots = Slots.Where(s => s.IsAvailable).ToList();

            return availableSlots;
        }
        
        private IEnumerable<Slot> BuildSlots(
            IEnumerable<Slot> appointments,
            TimeSpan slotDuration)
        {
            var slots = new List<Slot>();
            var _activeWorkingTimeRanges = new (DateTime Start, DateTime End)[]
            {
                (_workSchedule.StartHour.ToDateTime(_date), _workSchedule.BreakStartHour.ToDateTime(_date)),
                (_workSchedule.BreakEndHour.ToDateTime(_date), _workSchedule.EndHour.ToDateTime(_date))               
            };

            for(var i = 0; i<_activeWorkingTimeRanges.Length; i++)
            {
                var startPeriod = _activeWorkingTimeRanges[i].Start;
                var endPeriod = _activeWorkingTimeRanges[i].End;

                for(var j = startPeriod; j < endPeriod; j += slotDuration)
                {
                    var endSlot = j + slotDuration;
                    if(endSlot > endPeriod)
                    {
                        break;
                    }

                    var newSlot = new Slot
                    {
                        Start = j,
                        End = endSlot
                    };
                    newSlot.IsAvailable = IsSlotAvailable(newSlot, appointments);
                    
                    slots.Add(newSlot);
                }
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
    }
}
