using BookingSystem.Domain.Schedule;
using System.Collections;

namespace BookingSystem.Domain.UnitTests.Schedule.TestData
{
    public class DailyScheduleTestData : IEnumerable<object[]>
    {
        private static DateOnly Date = CreateDefaultDate();

        public IEnumerator<object[]> GetEnumerator()
        {
            // Test seven slots should be arranged and available when there are no appointments booked
            yield return new object[] {
                new DailyScheduleTestDataScenario
                {
                    Date = Date,
                    DailyWorkSchedule = CreateDefaultDailyWorkSchedule(),
                    SlotDuration = TimeSpan.FromHours(1),
                    Appointments = Enumerable.Empty<Slot>(),
                    ExpectedSlots = new Slot[]
                    {
                        CreateSlot(Date, startTime: new TimeOnly(10,0), endTime: new TimeOnly(11,0), isSlotAvailable: true),
                        CreateSlot(Date, startTime: new TimeOnly(11,0), endTime: new TimeOnly(12,0), isSlotAvailable: true),
                        CreateSlot(Date, startTime: new TimeOnly(12,0), endTime: new TimeOnly(13,0), isSlotAvailable: true),
                        CreateSlot(Date, startTime: new TimeOnly(14,0), endTime: new TimeOnly(15,0), isSlotAvailable: true),
                        CreateSlot(Date, startTime: new TimeOnly(15,0), endTime: new TimeOnly(16,0), isSlotAvailable: true),
                        CreateSlot(Date, startTime: new TimeOnly(16,0), endTime: new TimeOnly(17,0), isSlotAvailable: true),
                        CreateSlot(Date, startTime: new TimeOnly(17,0), endTime: new TimeOnly(18,0), isSlotAvailable: true)
                    } 
                }
            };
            // Test seven slots should be arranged, three of them available based on the current appointments booked.
            yield return new object[] {
                new DailyScheduleTestDataScenario
                {
                    Date = Date,
                    DailyWorkSchedule = CreateDefaultDailyWorkSchedule(),
                    SlotDuration = TimeSpan.FromHours(1),
                    Appointments = new Slot[]
                    {                        
                        CreateSlot(Date, startTime: new TimeOnly(12,0), endTime: new TimeOnly(13,0)),
                        CreateSlot(Date, startTime: new TimeOnly(14,0), endTime: new TimeOnly(15,0)),
                        CreateSlot(Date, startTime: new TimeOnly(15,0), endTime: new TimeOnly(16,0)),
                        CreateSlot(Date, startTime: new TimeOnly(17,0), endTime: new TimeOnly(18,0))
                    },
                    ExpectedSlots = new Slot[]
                    {
                        CreateSlot(Date, startTime: new TimeOnly(10,0), endTime: new TimeOnly(11,0), isSlotAvailable: true),
                        CreateSlot(Date, startTime: new TimeOnly(11,0), endTime: new TimeOnly(12,0), isSlotAvailable: true),
                        CreateSlot(Date, startTime: new TimeOnly(12,0), endTime: new TimeOnly(13,0)),
                        CreateSlot(Date, startTime: new TimeOnly(14,0), endTime: new TimeOnly(15,0)),
                        CreateSlot(Date, startTime: new TimeOnly(15,0), endTime: new TimeOnly(16,0)),
                        CreateSlot(Date, startTime: new TimeOnly(16,0), endTime: new TimeOnly(17,0), isSlotAvailable: true),
                        CreateSlot(Date, startTime: new TimeOnly(17,0), endTime: new TimeOnly(18,0))
                    }
                }
            };
            // Test seven slots should be arranged but none available based on the current appointments booked.
            yield return new object[] {
                new DailyScheduleTestDataScenario
                {
                    Date = Date,
                    DailyWorkSchedule = CreateDefaultDailyWorkSchedule(),
                    SlotDuration = TimeSpan.FromHours(1),
                    Appointments = new Slot[]
                    {
                        CreateSlot(Date, startTime: new TimeOnly(10,0), endTime: new TimeOnly(11,0)),
                        CreateSlot(Date, startTime: new TimeOnly(11,0), endTime: new TimeOnly(12,0)),
                        CreateSlot(Date, startTime: new TimeOnly(12,0), endTime: new TimeOnly(13,0)),
                        CreateSlot(Date, startTime: new TimeOnly(14,0), endTime: new TimeOnly(15,0)),
                        CreateSlot(Date, startTime: new TimeOnly(15,0), endTime: new TimeOnly(16,0)),
                        CreateSlot(Date, startTime: new TimeOnly(16,0), endTime: new TimeOnly(17,0)),
                        CreateSlot(Date, startTime: new TimeOnly(17,0), endTime: new TimeOnly(18,0))
                    },
                    ExpectedSlots = new Slot[]
                    {
                        CreateSlot(Date, startTime: new TimeOnly(10,0), endTime: new TimeOnly(11,0)),
                        CreateSlot(Date, startTime: new TimeOnly(11,0), endTime: new TimeOnly(12,0)),
                        CreateSlot(Date, startTime: new TimeOnly(12,0), endTime: new TimeOnly(13,0)),
                        CreateSlot(Date, startTime: new TimeOnly(14,0), endTime: new TimeOnly(15,0)),
                        CreateSlot(Date, startTime: new TimeOnly(15,0), endTime: new TimeOnly(16,0)),
                        CreateSlot(Date, startTime: new TimeOnly(16,0), endTime: new TimeOnly(17,0)),
                        CreateSlot(Date, startTime: new TimeOnly(17,0), endTime: new TimeOnly(18,0))
                    }
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private static DateOnly CreateDefaultDate()
        {
            var date = DateOnly.FromDateTime(DateTime.Now);

            return date;
        }

        private static DailyWorkSchedule CreateDefaultDailyWorkSchedule()
        {
            var dailyWorkSchedule = new DailyWorkSchedule
            {
                StartHour = new TimeOnly(10, 0),
                BreakStartHour = new TimeOnly(13, 0),
                BreakEndHour = new TimeOnly(14, 0),
                EndHour = new TimeOnly(18, 0),
            };

            return dailyWorkSchedule;
        }

        private static Slot CreateSlot(DateOnly date, TimeOnly startTime, TimeOnly endTime, bool isSlotAvailable = false)
        {
            var slot = new Slot
            {
                Start = new DateTime(date.Year, date.Month, date.Day, startTime.Hour, startTime.Minute, startTime.Second),
                End = new DateTime(date.Year, date.Month, date.Day, endTime.Hour, endTime.Minute, endTime.Second),
                IsAvailable = isSlotAvailable
            };

            return slot;
        }
    }
}
