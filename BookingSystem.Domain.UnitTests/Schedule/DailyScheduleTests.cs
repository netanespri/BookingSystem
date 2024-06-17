using AutoFixture.Xunit2;
using BookingSystem.Domain.Schedule;
using BookingSystem.Domain.UnitTests.Schedule.Assertions;
using BookingSystem.Domain.UnitTests.Schedule.TestData;
using FluentAssertions;

namespace BookingSystem.Domain.UnitTests.Schedule
{
    public class DailyScheduleTests
    {
        private static DateOnly DefaultDate = DailyScheduleTestDataScenario.CreateDefaultDate();

        [Theory]
        [AutoData]
        public void When_Daily_Schedule_Created_Then_The_Day_Is_Set(
            DateTime dateTime,
            DailyWorkSchedule dailyWorkSchedule,
            IEnumerable<Slot> appointments,
            TimeSpan slotDuration)
        {
            // Arrange
            var expectedDate = DateOnly.FromDateTime(dateTime);

            // Act
            var sut = new DailySchedule(
                                expectedDate,
                                dailyWorkSchedule,
                                appointments,
                                slotDuration);

            // Assert
            sut.DayName.Should().Be(expectedDate.DayOfWeek);
        }

        [Theory]        
        [MemberData(nameof(AllSlotsAreAvailableWhenNoAppointmentsBooked))]
        public void When_Daily_Schedule_Created_And_No_Appointments_Booked_Then_Slots_Are_Arranged_And_All_Available(
            DailyScheduleTestDataScenario dailyScheduleTestDataScenario)
        {
            // Act
            var sut = new DailySchedule(
                                dailyScheduleTestDataScenario.Date,
                                dailyScheduleTestDataScenario.DailyWorkSchedule,
                                dailyScheduleTestDataScenario.Appointments,
                                dailyScheduleTestDataScenario.SlotDuration);

            // Assert
            sut.Assert(dailyScheduleTestDataScenario);
        }

        // Data to test seven slots that should be arranged and available when there are no appointments booked
        public static IEnumerable<object[]> AllSlotsAreAvailableWhenNoAppointmentsBooked =>
            new List<object[]>
            {
                new object[] {
                    new DailyScheduleTestDataScenario
                    {
                        Date = DefaultDate,
                        DailyWorkSchedule = CreateDefaultDailyWorkSchedule(),
                        SlotDuration = TimeSpan.FromHours(1),
                        Appointments = Enumerable.Empty<Slot>(),
                        ExpectedSlots = new Slot[]
                        {
                            CreateSlot(DefaultDate, startTime: new TimeOnly(10,0), endTime: new TimeOnly(11,0), isSlotAvailable: true),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(11,0), endTime: new TimeOnly(12,0), isSlotAvailable: true),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(12,0), endTime: new TimeOnly(13,0), isSlotAvailable: true),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(14,0), endTime: new TimeOnly(15,0), isSlotAvailable: true),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(15,0), endTime: new TimeOnly(16,0), isSlotAvailable: true),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(16,0), endTime: new TimeOnly(17,0), isSlotAvailable: true),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(17,0), endTime: new TimeOnly(18,0), isSlotAvailable: true)
                        }
                    }
                }
            };

        [Theory]
        [MemberData(nameof(SevenSlotsArrangedAndThreeAvailableWhenFourAppointmentsBooked))]
        public void When_Daily_Schedule_Created_And_Some_Appointments_Booked_Then_Slots_Are_Arranged_And_Some_Available(
            DailyScheduleTestDataScenario dailyScheduleTestDataScenario)
        {
            // Act
            var sut = new DailySchedule(
                                dailyScheduleTestDataScenario.Date,
                                dailyScheduleTestDataScenario.DailyWorkSchedule,
                                dailyScheduleTestDataScenario.Appointments,
                                dailyScheduleTestDataScenario.SlotDuration);

            // Assert
            sut.Assert(dailyScheduleTestDataScenario);
        }

        // Data to test seven slots that should be arranged, three of them available based on the current appointments booked.
        public static IEnumerable<object[]> SevenSlotsArrangedAndThreeAvailableWhenFourAppointmentsBooked =>
            new List<object[]>
            {
                new object[] {
                    new DailyScheduleTestDataScenario
                    {
                        Date = DefaultDate,
                        DailyWorkSchedule = CreateDefaultDailyWorkSchedule(),
                        SlotDuration = TimeSpan.FromHours(1),
                        Appointments = new Slot[]
                        {
                            CreateSlot(DefaultDate, startTime: new TimeOnly(12,0), endTime: new TimeOnly(13,0)),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(14,0), endTime: new TimeOnly(15,0)),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(15,0), endTime: new TimeOnly(16,0)),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(17,0), endTime: new TimeOnly(18,0))
                        },
                        ExpectedSlots = new Slot[]
                        {
                            CreateSlot(DefaultDate, startTime: new TimeOnly(10,0), endTime: new TimeOnly(11,0), isSlotAvailable: true),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(11,0), endTime: new TimeOnly(12,0), isSlotAvailable: true),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(12,0), endTime: new TimeOnly(13,0)),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(14,0), endTime: new TimeOnly(15,0)),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(15,0), endTime: new TimeOnly(16,0)),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(16,0), endTime: new TimeOnly(17,0), isSlotAvailable: true),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(17,0), endTime: new TimeOnly(18,0))
                        }
                    }
                }
            };

        [Theory]
        [MemberData(nameof(SevenSlotsArrangedAndNoneAvailableWhenSevenAppointmentsBooked))]
        public void When_Daily_Schedule_Created_And_All_Appointments_Booked_Then_Slots_Are_Arranged_And_None_Available(
            DailyScheduleTestDataScenario dailyScheduleTestDataScenario)
        {
            // Act
            var sut = new DailySchedule(
                                dailyScheduleTestDataScenario.Date,
                                dailyScheduleTestDataScenario.DailyWorkSchedule,
                                dailyScheduleTestDataScenario.Appointments,
                                dailyScheduleTestDataScenario.SlotDuration);

            // Assert
            sut.Assert(dailyScheduleTestDataScenario);
        }

        // Data to test seven slots that should be arranged but none available based on the current appointments booked.
        public static IEnumerable<object[]> SevenSlotsArrangedAndNoneAvailableWhenSevenAppointmentsBooked =>
            new List<object[]>
            {
                new object[] {
                    new DailyScheduleTestDataScenario
                    {
                        Date = DefaultDate,
                        DailyWorkSchedule = CreateDefaultDailyWorkSchedule(),
                        SlotDuration = TimeSpan.FromHours(1),
                        Appointments = new Slot[]
                        {
                            CreateSlot(DefaultDate, startTime: new TimeOnly(10,0), endTime: new TimeOnly(11,0)),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(11,0), endTime: new TimeOnly(12,0)),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(12,0), endTime: new TimeOnly(13,0)),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(14,0), endTime: new TimeOnly(15,0)),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(15,0), endTime: new TimeOnly(16,0)),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(16,0), endTime: new TimeOnly(17,0)),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(17,0), endTime: new TimeOnly(18,0))
                        },
                        ExpectedSlots = new Slot[]
                        {
                            CreateSlot(DefaultDate, startTime: new TimeOnly(10,0), endTime: new TimeOnly(11,0)),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(11,0), endTime: new TimeOnly(12,0)),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(12,0), endTime: new TimeOnly(13,0)),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(14,0), endTime: new TimeOnly(15,0)),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(15,0), endTime: new TimeOnly(16,0)),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(16,0), endTime: new TimeOnly(17,0)),
                            CreateSlot(DefaultDate, startTime: new TimeOnly(17,0), endTime: new TimeOnly(18,0))
                        }
                    }
                }
            };

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